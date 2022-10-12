using FluentValidation;
using InventoryManager.Domain;
using InventoryManager.Infraestructure.QueriesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infraestructure.Validators
{
    public class WarehouseValidator : AbstractValidator<Warehouse>
    {
        /// <summary>
        /// Validator for Warehouse
        /// </summary>
        public WarehouseValidator()
        {
            RuleFor(w => w.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(w => w.Items)
                .NotNull();
        }

        /// <summary>
        /// Validate if an item can be added
        /// </summary>
        /// <param name="warehouse">a warehouse</param>
        /// <param name="warehouseItem">a warehouse item</param>
        /// <exception cref="ValidationException"></exception>
        internal void ValidateIfCanAddItem(Warehouse warehouse, WarehouseItem warehouseItem)
        {
            RuleFor(w => w.Items).Custom((list, context) =>
            {
                if (list.Any(x => x.Product.Code == warehouseItem.Product.Code))

                    context.AddFailure($"The product {warehouseItem.Product.Code} exists in the warehouse and can't be added");

            });

            var result = Validate(warehouse);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

        }

        /// <summary>
        /// Validate if an item can be delete
        /// </summary>
        /// <param name="warehouse"></param>
        /// <param name="warehouseItem"></param>
        internal void ValidateIfCanRemoveItem(Warehouse warehouse, WarehouseItem? warehouseItem)
        {

        }
    }

}
