
namespace InventoryManager.Domain
{
    public class Warehouse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public ICollection<WarehouseItem>? Items { get; set; } = new List<WarehouseItem>();
    }
}