using FluentValidation;
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
        public IActionResult GetInventory()
        {
            try
            {
                var inventoryList = _inventoryApplication.GetInventory(null);
                return Ok(inventoryList);
            }
            // Excepciones controladas 
            catch (Exception ex) when (ex is ApplicationException ||
                                        ex is ValidationException)

            {
                _logger.LogError($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
                return Problem(
                    title: ex.Message,
                    detail: ex.StackTrace
                    ); 

            }
        }

        /// <summary>
        /// Get inventory of a warehouse
        /// </summary>
        /// <param name="warehouseCode">Warehouse Code (for example, "N" or "S")</param>
        /// <returns>A list of warehouse items</returns>
        [HttpGet("GetInventory/{warehouseCode}")]
        public IActionResult GetInventory(string warehouseCode)
        {
            try
            {
                var inventoryList = _inventoryApplication.GetInventory(warehouseCode);
                return Ok(inventoryList);
            }
            // Excepciones controladas 
            catch (Exception ex) when (ex is ApplicationException ||
                                        ex is ValidationException)

            {
                _logger.LogError($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
                return Problem(
                    title: ex.Message,
                    detail: ex.StackTrace
                );
            }
        }

        /// <summary>
        /// Add an item to a warehouse inventory
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="quantity">quantity of a product</param>
        /// <param name="warehouseId">Id of the warehouse</param>
        [HttpPost("AddInventory")]
        public IActionResult AddInventory(Guid productId, int quantity, Guid warehouseId)
        {
            try
            {
                _inventoryApplication.AddInventory(productId, quantity, warehouseId);
                return Ok();
            }
            // Excepciones controladas 
            catch (Exception ex) when (ex is ApplicationException ||
                                        ex is ValidationException)
            {
                _logger.LogError($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
                return Problem(
                    title: ex.Message,
                    detail: ex.StackTrace
                );
            }
        }


        /// <summary>
        /// Delete an item to a warehouse inventory
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="warehouseId">Id of the warehouse</param>
        [HttpPost("DeleteInventory")]
        public IActionResult DeleteInventory(Guid productId, Guid warehouseId)
        {
            try
            {
                _inventoryApplication.DeleteInventory(productId, warehouseId);
                return Ok();
            }
            // Excepciones controladas 
            catch (Exception ex) when (ex is ApplicationException ||
                                        ex is ValidationException)

            {
                _logger.LogError($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
                return Problem(
                    title: ex.Message,
                    detail: ex.StackTrace
                );
            }
        }


        /// <summary>
        /// Modify an item to a warehouse inventory
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="warehouseId">Id of the warehouse</param>
        /// <param name="quantity">Quantity of the product</param>
        [HttpPost("ModifyInventory")]
        public IActionResult ModifyInventory(Guid productId, int quantity, Guid warehouseId)
        {
            try
            {
                _inventoryApplication.ModifyInventory(productId, quantity, warehouseId);
                return Ok();
            }
            // Excepciones controladas 
            catch (Exception ex) when (ex is ApplicationException ||
                                        ex is ValidationException)

            {
                _logger.LogError($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            //Excepciones no controladas
            catch (Exception ex)
            {
                _logger.LogError($"Error no controlado: {ex.Message} :{ex.StackTrace}");
                return Problem(
                    title: ex.Message,
                    detail: ex.StackTrace
                );
            }
        }
    }
}