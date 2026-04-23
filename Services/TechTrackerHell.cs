using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using TechTrackerBlazor.Models;
using TechTrackerBlazor.Settings; 

namespace TechTrackerBlazor.Services;

public class TechTrackerHell
{
    private static readonly string[] ActiveRepairStatuses = ["Pending", "In Progress", "Completed"];
    private readonly IMongoCollection<Customer> customers;
    private readonly IMongoCollection<DeviceAsset> devices;
    private readonly IMongoCollection<Employee> employees;
    private readonly IMongoCollection<InventoryItem> inventory;
    private readonly IMongoCollection<RepairOrder> repairs;
    private readonly IMongoCollection<TransactionRecord> transactions;

    public TechTrackerHell(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);

        customers = database.GetCollection<Customer>("Customers");
        devices = database.GetCollection<DeviceAsset>("Devices");
        employees = database.GetCollection<Employee>("Employees");
        inventory = database.GetCollection<InventoryItem>("Inventory");
        repairs = database.GetCollection<RepairOrder>("RepairOrders");
        transactions = database.GetCollection<TransactionRecord>("Transactions");
    }
    // --- DASHBOARD ANALYTICS METHODS ---
    // Requirement: Average repair turnaround times
    public async Task<double> GetAverageTurnaroundTimeAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument("Status", "Completed")),
            new BsonDocument("$project", new BsonDocument
            {
                { "Duration", new BsonDocument("$subtract", new BsonArray { "$CompletionDate", "$CreationDate" }) }
            }),
            new BsonDocument("$group", new BsonDocument
            {
                { "_id", BsonNull.Value },
                { "AvgTime", new BsonDocument("$avg", "$Duration") }
            })
        };
        var result = await repairs.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
        if (result == null || !result.Contains("AvgTime")) return 0;
        double ms = result["AvgTime"].ToDouble();
        return Math.Round(ms / (1000 * 60 * 60 * 24), 1); 
    }

    // Requirement: Most frequent repair types (Bar Chart Data)
    public async Task<Dictionary<string, int>> GetRepairTypeStatsAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$group", new BsonDocument { 
                { "_id", "$Status" }, 
                { "count", new BsonDocument("$sum", 1) } 
            })
        };
        
        var result = await repairs.Aggregate<BsonDocument>(pipeline).ToListAsync();
        return result.ToDictionary(x => x["_id"].ToString(), x => x["count"].AsInt32);
    }

    // Requirement: Monthly revenue trends (Line Chart Data)
    public async Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync()
    {
        var result = await transactions.Find(t => t.Completed).ToListAsync();

        return result.GroupBy(t => t.Date.ToString("MMM yyyy"))
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Cost));
    }

    // --- EXISTING DATA METHODS ---

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await customers.Find(_ => true).ToListAsync();
    }
}
