using FluentValidation;
using InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infraestructure.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        /// <summary>
        /// Validator for product
        /// </summary>
        public ProductValidator()
        {

            RuleFor(w => w.Id)
                .NotNull();

            RuleFor(w => w.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(w => w.Price)
                .NotEmpty()
                .GreaterThan(0)
                .ScalePrecision(2, 7);

        }
    }
}

