using FluentValidation;
using InventoryManager.Application.Mappers;
using InventoryManager.Application.Views;
using InventoryManager.CrossCutting.Exceptions;
using InventoryManager.Domain;
using InventoryManager.Infraestructure.CommandsRepository;
using InventoryManager.Infraestructure.QueriesRepository;

namespace InventoryManager.Application
{
    /// <summary>
    /// Clase 
    /// </summary>
    public class InventoryApplication : IInventoryApplication
    {
        private IInventoryQueriesRepository _inventoryQueriesRepository;
        private IInventoryCommandsRepository _inventoryCommandsRepository;

        public InventoryApplication(IInventoryCommandsRepository inventoryCommandsRepository,
            IInventoryQueriesRepository inventoryQueriesRepository)
        {
            _inventoryQueriesRepository = inventoryQueriesRepository;
            _inventoryCommandsRepository = inventoryCommandsRepository;

        }

        /// <summary>
        /// Add a product to an inventory
        /// </summary>
        /// <param name="productId">Id of a product</param>
        /// <param name="quatity">quantity of a product</param>
        /// <param name="warehouseId">Id of a warehouse</param>
        public void AddInventory(Guid productId, int quatity, Guid warehouseId)
        {
            _inventoryCommandsRepository.AddInventory(productId, quatity, warehouseId);
        }

        /// <summary>
        /// Delete a product from an inventory
        /// </summary>
        /// <param name="productId">Id of a product</param>
        /// <param name="warehouseId">Id of a warehouse</param>
        public void DeleteInventory(Guid productId, Guid warehouseId)
        {
            _inventoryCommandsRepository.DeleteInventory(productId, warehouseId);
        }


        /// <summary>
        /// Modify quantity of a product from an inventory
        /// </summary>
        /// <param name="productId">Id of a product</param>
        /// <param name="warehouseId">Id of a warehouse</param>
        /// <param name="quatity">Quantity</param>
        public void ModifyInventory(Guid productId, int quatity, Guid warehouseId)
        {
            _inventoryCommandsRepository.ModifyInventory(productId, quatity, warehouseId);
        }





        /// <summary>
        /// Get an inventoryList from a warehouse. If warehouse is null, obtain data from all warehouse
        /// </summary>
        /// <param name="warehouseCode">warehouse Code</param>
        /// <returns>View of inventory</returns>
        public InventoryItemListView GetInventory(string? warehouseCode)
        {
            var warehouseName = string.IsNullOrEmpty(warehouseCode) ? "TODOS" : GetWarehouseName(warehouseCode);

            var inventory = _inventoryQueriesRepository.GetInventory(warehouseCode);

            // Podría haber usado Automapper
            var inventoryitemListView = MapperService.MapWareHouseItemsToView(inventory);

            inventoryitemListView.warehouseName = warehouseName;

            return inventoryitemListView;
        }


        /// <summary>
        /// Obtain product by code
        /// </summary>
        /// <param name="code">code of product</param>
        /// <returns>a product</returns>
        public Product GetProductByCode(string code)
        {
            return _inventoryQueriesRepository.GetProductByCode(code);
        }

        /// <summary>
        /// Obtain warehouse by code
        /// </summary>
        /// <param name="code">code of warehouse</param>
        /// <returns>a warehouse</returns>
        public Warehouse GetWarehouseByCode(string code)
        {
            return _inventoryQueriesRepository.GetWarehouseByCode(code);
        }



        /// <summary>
        /// Get warehouse name
        /// </summary>
        /// <param name="value">warehouse code</param>
        /// <returns>name of warehouse</returns>
        private string GetWarehouseName(string code)
        {
            var name = _inventoryQueriesRepository.GetWarehouseByCode(code)?.Name;

            return name;

        }
    }
}