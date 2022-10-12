namespace InventoryManager.Application.Views
{
    /// <summary>
    /// Data used by frontend
    /// </summary>
    public class InventoryItemListView
    {
        public ICollection<InventoryItemView> inventoryItems { get; set; } = new List<InventoryItemView>();
        public decimal amount { get; set; }
        public string? warehouseName { get; set; }

    }
}