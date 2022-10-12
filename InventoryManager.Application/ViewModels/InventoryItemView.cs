namespace InventoryManager.Application.Views
{
    /// <summary>
    /// Data used by Frontend
    /// </summary>
    public class InventoryItemView
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string ProductCode { get; internal set; }
        public string WarehouseCode { get; internal set; }
        public string WarehouseName { get; internal set; }
    }
}