using InventoryManager.Application.Views;
using InventoryManager.Domain;
using System.Net.WebSockets;

namespace InventoryManager.Application.Mappers
{
    internal class MapperService
    {
        /// <summary>
        /// Maps a list of warehouses into a inventoryItemListView used by frontEnd
        /// </summary>
        /// <param name="warehouses">List of warehouses, data from context</param>
        /// <returns>Data used by front</returns>
        internal static InventoryItemListView MapWareHouseItemsToView(IEnumerable<Warehouse> warehouses)
        {
            var resp = new InventoryItemListView();

            foreach (var warehouse in warehouses)
            {
                foreach (var item in warehouse.Items)
                {
                    resp.inventoryItems.Add(
                        new InventoryItemView
                        {
                            ProductCode = item.Product.Code,
                            Name = item.Product.Name,
                            Quantity = item.Quantity,
                            Price = item.Product.Price,
                            Amount = item.Quantity * item.Product.Price,
                            WarehouseCode = warehouse.Code,
                            WarehouseName = warehouse.Name
                        });
                }
            }
            resp.amount = resp.inventoryItems.Sum(i => i.Amount);

            return resp;
        }
    }
}