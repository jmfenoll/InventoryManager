using InventoryManager.Domain;

namespace InventoryManager.Infraestructure.CommandsRepository
{
    public interface IInventoryCommandsRepository
    {
        void AddInventory(Guid productId, int quatity, Guid warehouseId);
        void DeleteInventory(Guid productId, Guid warehouseId);
        void ModifyInventory(Guid productId, int quatity, Guid warehouseId);
    }
}