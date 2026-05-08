using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using TechTrackerBlazor.Models;
using TechTrackerBlazor.Settings; 

namespace TechTrackerBlazor.Services;

public class TechTrackerHell
{
    private static readonly string[] ActiveRepairStatuses = ["Pending", "In Progress", "Completed"];
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<Customer> customers;
    private readonly IMongoCollection<DeviceAsset> devices;
    private readonly IMongoCollection<Employee> employees;
    private readonly IMongoCollection<InventoryItem> inventory;
    private readonly IMongoCollection<RepairOrder> repairs;
    private readonly IMongoCollection<TransactionRecord> transactions;

    public TechTrackerHell(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        database = client.GetDatabase(settings.Value.DatabaseName);

        customers = database.GetCollection<Customer>("Customers");
        devices = database.GetCollection<DeviceAsset>("Devices");
        employees = database.GetCollection<Employee>("Employees");
        inventory = database.GetCollection<InventoryItem>("Inventory");
        repairs = database.GetCollection<RepairOrder>("RepairOrders");
        transactions = database.GetCollection<TransactionRecord>("Transactions");
    }

   ///GENERATED WITH CHATGPT FOR FINAL TEST DATA INSERTION - start
    public async Task SeedTestDataAsync()
    {
        var collectionNames = new[] { "Customers", "Devices", "Employees", "Inventory", "RepairOrders", "Transactions" };

        foreach (var collectionName in collectionNames)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            if (await collection.CountDocumentsAsync(FilterDefinition<BsonDocument>.Empty) > 0)
            {
                return;
            }
        }

        var seedPath = Path.Combine(AppContext.BaseDirectory, "TestData", "seed-data.json");
        if (!File.Exists(seedPath))
        {
            seedPath = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "seed-data.json");
        }

        if (!File.Exists(seedPath))
        {
            return;
        }

        var seedData = BsonDocument.Parse(await File.ReadAllTextAsync(seedPath));
        var seedCount = seedData.GetValue("SeedCount", 25).ToInt32();

        foreach (var collectionName in collectionNames)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var template = seedData[collectionName].AsBsonDocument;
            var documents = Enumerable.Range(0, seedCount)
                .Select(index =>
                {
                    var document = new BsonDocument(template);
                    if (index > 0)
                    {
                        document["_id"] = ObjectId.GenerateNewId();
                    }

                    return document;
                })
                .ToList();

            if (documents.Count > 0)
            {
                await collection.InsertManyAsync(documents);
            }
        }
    }
    ///GENERATED WITH CHATGPT FOR FINAL TEST DATA INSERTION - finish

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

    
    public async Task<Dictionary<string, int>> GetRepairTypeStatsAsync()
    {
        var stats = new Dictionary<string, int>
        {
            { "Pending", 0 },
            { "In Progress", 0 },
            { "Completed", 0 }
        };

        var pipeline = new[]
        {
            new BsonDocument("$group", new BsonDocument { 
                { "_id", "$Status" }, 
                { "count", new BsonDocument("$sum", 1) } 
            })
        };
        
        var result = await repairs.Aggregate<BsonDocument>(pipeline).ToListAsync();

        
        foreach (var doc in result)
        {
            
            var status = doc["_id"].IsBsonNull ? "Unknown" : doc["_id"].AsString;
            
            if (stats.ContainsKey(status))
            {
                stats[status] = doc["count"].AsInt32;
            }
        }

        return stats;
    }

    public async Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync()
    {
        var result = await transactions.Find(t => t.Completed).ToListAsync();

        return result.GroupBy(t => t.Date.ToString("MMM yyyy"))
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Cost));
    }


    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await customers.Find(_ => true).ToListAsync();
    }

    public async Task CreateCustomerAsync(Customer newCustomer)
    {
        await customers.InsertOneAsync(newCustomer);
    }

    public async Task DeleteCustomerAsync(string id)
    {
        await customers.DeleteOneAsync(c => c.Id == id);
    }

    public async Task UpdateCustomerAsync(Customer updatedCustomer)
    {
        await customers.ReplaceOneAsync(c => c.Id == updatedCustomer.Id, updatedCustomer);
    }

    public async Task<List<RepairOrder>> GetAllRepairsAsync()
    {
        return await repairs.Find(_ => true).ToListAsync();
    }

    public async Task CreateRepairAsync(RepairOrder newRepair)
    {
        newRepair.CreationDate = DateTime.UtcNow;
        await repairs.InsertOneAsync(newRepair);
    }

    public async Task UpdateRepairAsync(RepairOrder updatedRepair)
    {
        updatedRepair.UpdateDate = DateTime.UtcNow;
        if (updatedRepair.Status == "Completed" && updatedRepair.CompletionDate == null)
        {
            updatedRepair.CompletionDate = DateTime.UtcNow;
        }
        await repairs.ReplaceOneAsync(r => r.Id == updatedRepair.Id, updatedRepair);
    }

    public async Task DeleteRepairAsync(string id)
    {
        await repairs.DeleteOneAsync(r => r.Id == id);
    }

    public async Task<List<InventoryItem>> GetAllInventoryAsync()
    {
        return await inventory.Find(_ => true).ToListAsync();
    }

    public async Task CreateInventoryItemAsync(InventoryItem newItem)
    {
        await inventory.InsertOneAsync(newItem);
    }

    public async Task UpdateInventoryItemAsync(InventoryItem updatedItem)
    {
        await inventory.ReplaceOneAsync(i => i.Id == updatedItem.Id, updatedItem);
    }

    public async Task DeleteInventoryItemAsync(string id)
    {
        await inventory.DeleteOneAsync(i => i.Id == id);
    }
    public async Task<List<DeviceAsset>> GetAllDevicesAsync()
    {
        return await devices.Find(_ => true).ToListAsync();
    }

    public async Task CreateDeviceAsync(DeviceAsset newDevice)
    {
        await devices.InsertOneAsync(newDevice);
    }

    public async Task UpdateDeviceAsync(DeviceAsset updatedDevice)
    {
        await devices.ReplaceOneAsync(d => d.Id == updatedDevice.Id, updatedDevice);
    }

    public async Task DeleteDeviceAsync(string id)
    {
        await devices.DeleteOneAsync(d => d.Id == id);
    }

    public async Task<List<TransactionRecord>> GetAllTransactionsAsync()
    {
        return await transactions.Find(_ => true).SortByDescending(t => t.Date).ToListAsync();
    }

    public async Task CreateTransactionAsync(TransactionRecord newTransaction)
    {
        newTransaction.Date = DateTime.UtcNow; 
        await transactions.InsertOneAsync(newTransaction);
    }

    public async Task RefundTransactionAsync(string id)
    {
        var update = Builders<TransactionRecord>.Update.Set(t => t.Completed, false);
        await transactions.UpdateOneAsync(t => t.Id == id, update);
    }

    public async Task MarkTransactionPaidAsync(string id)
    {
        var update = Builders<TransactionRecord>.Update.Set(t => t.Completed, true);
        await transactions.UpdateOneAsync(t => t.Id == id, update);
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await employees.Find(_ => true).ToListAsync();
    }

    public async Task CreateEmployeeAsync(Employee newEmployee)
    {
        await employees.InsertOneAsync(newEmployee);
    }

    public async Task UpdateEmployeeAsync(Employee updatedEmployee)
    {
        await employees.ReplaceOneAsync(e => e.Id == updatedEmployee.Id, updatedEmployee);
    }

    public async Task DeleteEmployeeAsync(string id)
    {
        await employees.DeleteOneAsync(e => e.Id == id);
    }
    
}
