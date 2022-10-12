using FluentValidation;
using InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infraestructure.Validators
{
    /// <summary>
    /// Validator for warehouseItem
    /// </summary>
    public class WarehouseItemValidator: AbstractValidator<WarehouseItem>
    {
        public WarehouseItemValidator()
        {
            RuleFor(w => w.Id)
                .NotNull();

            RuleFor(w => w.Quantity)
                .GreaterThanOrEqualTo(0);

            RuleFor(w => w.Product)
                .NotNull()
                .SetValidator(new ProductValidator());

        }

        /// <summary>
        /// Validate if item can be modified
        /// </summary>
        /// <param name="warehouse">a warehouse</param>
        /// <param name="item">an item</param>
        /// <param name="quatity">Quantity for update</param>
        /// <exception cref="ValidationException"></exception>
        internal void ValidateIfCanModifyItem(Warehouse warehouse, WarehouseItem item, int quatity)
        {


            var itemToValidate = new WarehouseItem
            {
                Id = item.Id,
                Product = new Product
                {
                    Id = item.Product.Id,
                    Code = item.Product.Code,
                    Name = item.Product.Name,
                    Price = item.Product.Price
                },
                Quantity = quatity
            };

            var result = Validate(itemToValidate);

            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }

}

