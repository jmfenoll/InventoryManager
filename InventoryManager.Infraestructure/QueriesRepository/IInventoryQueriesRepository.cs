using InventoryManager.Domain;

namespace InventoryManager.Infraestructure.QueriesRepository
{
    public interface IInventoryQueriesRepository
    {
        IEnumerable<Warehouse> GetInventory(string? warehouseCode);
        Product GetProductByCode(string code);
        Warehouse GetWarehouseByCode(string code);
    }
}