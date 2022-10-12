using FluentAssertions;
using IdentityModel.Client;
using InventoryManager.Application.Views;
using InventoryManager.Domain;
using InventoryManager.Infraestructure;
using InventoryManager.Infraestructure.Validators;
using InventoryManager.Tests.IntegrationTests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Xsl;

namespace InventoryManager.Tests.UnitTest
{
    /// <summary>
    /// Tests unitarios de BL, probaremos todas las causísticas posibles
    /// </summary>
    public class WarehouseDomainTest : CommonTest
    {
        [SetUp]
        public void Setup()
        {
            PrepareDI();
        }

        /// <summary>
        /// Un almacén no puede tener la descripción vacía
        /// </summary>
        [Test]
        public void ValidateWarehouse_EmptyName_ValidationIsRejected()
        {
            // ARRANGE
            Warehouse wh = new Warehouse()
            {
                Id = Guid.NewGuid(),
                Code = "N",
                Name = null,
                Items = new List<WarehouseItem>()
            };

            var validator = new WarehouseValidator();

            // ACT
            var result = validator.Validate(wh);

            // ASSERT
            result.IsValid.Should().BeFalse();
        }

        /// <summary>
        /// Un almacén no puede tener la descripción vacía
        /// </summary>
        [Test]
        public void ValidateWarehouseItem_NegativeQuantity_ValidationIsRejected()
        {
            // ARRANGE
            WarehouseItem whItem = new WarehouseItem()
            {
                Id = Guid.NewGuid(),
                Product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Code = "TORM5",
                    Name = "Tornillos Métrica 5",
                    Price = 12.99M
                },
                Quantity = -5
            };

            var validator = new WarehouseItemValidator();

            // ACT
            var result = validator.Validate(whItem);

            // ASSERT
            result.IsValid.Should().BeFalse();
        }

        /// <summary>
        /// Un producto no puede tener un precio negativo
        /// </summary>
        [Test]
        public void ValidateWarehouseItem_NegativePrice_ValidationIsRejected()
        {
            // ARRANGE
            WarehouseItem whItem = new WarehouseItem()
            {
                Id = Guid.NewGuid(),
                Product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Code = "TORM5",
                    Name = "Tornillos Métrica 5",
                    Price = -12.99M
                },
                Quantity = 5
            };

            var validator = new WarehouseItemValidator();

            // ACT
            var result = validator.Validate(whItem);

            // ASSERT
            result.IsValid.Should().BeFalse();
        }

    }

}