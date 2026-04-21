using MongoDB.Driver;
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

   
    }     
