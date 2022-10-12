using InventoryManager.Application;
using InventoryManager.Application.Views;
using InventoryManager.WebApi.Secure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventoryManager.WebApi.Controllers
{
    [BasicAuthorization]
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private IInventoryApplication _inventoryApplication;

        public InventoryController(
            ILogger<InventoryController> logger,
            IInventoryApplication inventoryApplication)
        {
            _logger = logger;
            _inventoryApplication = inventoryApplication;
        }

        /// <summary>
        /// Get inventory of all warehouses
        /// </summary>
        /// <returns>A list of warehouse items</returns>
        [HttpGet("GetInventory")]
        public InventoryItemListView GetInventory()
        {
            try
            {
                var inventoryList = _inventoryApplication.GetInventory(null);
                return inventoryList;
            }
            // Excepciones controladas 
            catch (ApplicationException ex)
            {
                _logger.LogError($"Error: {ex.Message}");
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
            }
            return null;
        }

        [HttpGet("GetInventory/{warehouse}")]
        public InventoryItemListView GetInventory(string warehouseCode)
        {
            var inventoryList = _inventoryApplication.GetInventory(warehouseCode);

            return inventoryList;
        }

    }
}