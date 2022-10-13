using FluentValidation;
using InventoryManager.Domain;
using InventoryManager.Infraestructure.Validators;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;

namespace InventoryManager.Infraestructure.CommandsRepository
{
    public class InventoryCommandsRepository : IInventoryCommandsRepository
    {
        private InventoryContext _context;

        public InventoryCommandsRepository(InventoryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a product to an Inventaru
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="quantity">Quantity of the product</param>
        /// <param name="warehouseId">Id of the warehouse</param>
        public void AddInventory(Guid productId, int quantity, Guid warehouseId)
        {

            var warehouse= _context.Warehouses.FirstOrDefault(x => x.Id == warehouseId);

            if (warehouse == null)
                throw new ValidationException("Warehouse incorrecto");


            var warehouseItem = new WarehouseItem
            {
                Id = Guid.NewGuid(),
                Product = _context.Products.FirstOrDefault(x => x.Id == productId),
                Quantity = quantity
            };

            if (warehouseItem.Product == null)
                throw new ValidationException("ProductId incorrecto");

            var validatorItem = new WarehouseItemValidator();
            validatorItem.ValidateAndThrow(warehouseItem);

            var validator = new WarehouseValidator();
            validator.ValidateIfCanAddItem(warehouse, warehouseItem);

            warehouse.Items.Add(warehouseItem);

        }

        /// <summary>
        /// Delete an item of an inventory
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="warehouseId">Id of the warehouse</param>
        /// <exception cref="ValidationException"></exception>
        public void DeleteInventory(Guid productId, Guid warehouseId)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(x => x.Id == warehouseId);

            if (warehouse == null)
                throw new ValidationException("Warehouse incorrecto");

            var warehouseItem = warehouse.Items.FirstOrDefault(x => x.Product.Id == productId);

            if (warehouseItem == null)
                throw new ValidationException("ProductId incorrecto");

            var validator = new WarehouseValidator();
            validator.ValidateIfCanRemoveItem(warehouse, warehouseItem);

            warehouse.Items.Remove(warehouseItem);
        }

        /// <summary>
        /// Modify quantity of an inventory
        /// </summary>
        /// <param name="productId">Id of productproduct</param>
        /// <param name="quatity">Quantity to modify</param>
        /// <param name="warehouseId">Id of warehouse</param>
        /// <exception cref="ValidationException"></exception>
        public void ModifyInventory(Guid productId, int quatity, Guid warehouseId)
        {
            var warehouse = _context.Warehouses.FirstOrDefault(x => x.Id == warehouseId);

            if (warehouse == null)
                throw new ValidationException("Warehouse incorrecto");


            var item = warehouse?.Items?.FirstOrDefault(x => x.Product.Id == productId);
            if (item == null)
                throw new ValidationException("ProductId incorrecto");

            var validator = new WarehouseItemValidator();            
            validator.ValidateIfCanModifyItem(warehouse, item, quatity);

            item.Quantity = quatity;


        }
    }
}



