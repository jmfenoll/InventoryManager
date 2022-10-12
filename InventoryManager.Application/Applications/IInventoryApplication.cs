using InventoryManager.Application.Views;
using InventoryManager.Domain;

namespace InventoryManager.Application
{
    public interface IInventoryApplication
    {
        void AddInventory(Guid productId, int quatity, Guid warehouseId);
        void DeleteInventory(Guid id1, Guid id2);
        void ModifyInventory(Guid id1, int v, Guid id2);

        InventoryItemListView GetInventory(string? warehouseCode);
        Product GetProductByCode(string code);
        Warehouse GetWarehouseByCode(string code);
        
    }
}