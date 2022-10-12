
namespace InventoryManager.Domain
{
    public class WarehouseItem
    {

        public WarehouseItem()
        {
            
        }

        public Guid Id { get; set; }
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }

    }
}