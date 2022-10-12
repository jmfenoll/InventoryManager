using FluentAssertions;
using FluentAssertions.Common;
using FluentValidation;
using InventoryManager.Application;

using InventoryManager.Application.Views;
using InventoryManager.CrossCutting.Exceptions;
using InventoryManager.Domain;
using InventoryManager.Infraestructure;
using InventoryManager.Infraestructure.CommandsRepository;
using InventoryManager.Infraestructure.QueriesRepository;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Xsl;

namespace InventoryManager.Tests.IntegrationTests
{
    /// <summary>
    /// Test de integraci�n a nivel de aplicaci�n.
    /// Tendremos un contexto de prueba que inyectaremos, as� no dependemos de la BD.
    /// </summary>
    public class InventoryManagerTestShould : CommonTest
    {

        [SetUp]
        public void Setup()
        {
            PrepareDI();

            PrepareDatabase();
        }

        /// <summary>
        /// Probamos el m�todo GetInventory y comprobamos que devuelve todos los datos
        /// </summary>
        [Test]
        public void GetAllInventory_WithoutParameter_ShouldReturnAllData()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();

            // ACT
            var result = application?.GetInventory(null);

            // ASSERT
            result.Should().NotBeNull();
            result?.warehouseName.Should().Be("TODOS");
            result?.inventoryItems.Should().HaveCount(5);
        }



        /// <summary>
        /// Probamos el m�todo GetInventory de un almac�n en concreto y comprobamos que devuelve los datos de ese almac�n
        /// </summary>
        [Test]
        public void GetAllInventory_WithValidWarehouse_ShouldReturnDataOfWarehouse()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();

            // ACT
            var result = application?.GetInventory("N");

            // ASSERT
            result.Should().NotBeNull();
            result?.warehouseName.Should().Match("*Norte*");
            result?.inventoryItems.Should().HaveCount(2);
        }

        /// <summary>
        /// Probamos con un almac�n no v�lido, debe devolvernos error
        /// </summary>
        [Test]
        public void GetAllInventory_WithWrongWarehouse_ShouldReturnException()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();

            // ACT
            Func<InventoryItemListView> result = () => application?.GetInventory("NOEXISTE");

            // ASSERT
            result.Should().Throw<DataNotFoundException>();
        }


        /// <summary>
        /// A�adimos un producto a un almac�n
        /// </summary>
        [Test]
        public void AddItemToInventory__ShouldBeAdded()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();
            var resultBeforeAdding = application?.GetInventory("S");

            // ACT
            application?.AddInventory(
                application.GetProductByCode("TUEM2").Id,
                15,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            var resultAfterAdding = application?.GetInventory("S");
            resultAfterAdding.inventoryItems.Count.Should().Be(resultBeforeAdding.inventoryItems.Count + 1);
        }

        /// <summary>
        /// A�adimos un producto existente a un almac�n. Debe de dar un error
        /// </summary>
        [Test]
        public void AddItemToInventory_ItemIsInInventory_ShouldThrowError()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();
            var resultBeforeAdding = application?.GetInventory("S");

            // ACT
            Action response= () => application?.AddInventory(
                application.GetProductByCode("TUEM4").Id,
                15,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            response.Should().Throw<ValidationException>().WithMessage("*product*can't be added*");
        }

        /// <summary>
        /// A�adimos un producto no dado de alta en un almac�n. Debe de dar un error
        /// </summary>
        [Test]
        public void AddItemToInventory_ProductNotExists_ShouldThrowError()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();
            var resultBeforeAdding = application?.GetInventory("S");

            // ACT
            Action response = () => application?.AddInventory(
                Guid.NewGuid(),
                15,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            response.Should().Throw<ValidationException>().WithMessage("*'Product' no debe estar vac�o*");
        }

        /// <summary>
        /// Quitamos un producto del almac�n
        /// </summary>
        [Test]
        public void DeleteItemFromToInventory__ShouldBeDeleted()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();
            var resultBeforeAdding = application?.GetInventory("S");

            // ACT
            application?.DeleteInventory(
                application.GetProductByCode("TORM4").Id,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            var resultAfterAdding = application?.GetInventory("S");
            resultAfterAdding.inventoryItems.Count.Should().Be(resultBeforeAdding.inventoryItems.Count - 1);
        }




        /// <summary>
        /// Modificamos la cantidad de un producto del almac�n
        /// </summary>
        [Test]
        public void ModifyItemFromToInventory__ShouldBeModified()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();

            // ACT
            application?.ModifyInventory(
                application.GetProductByCode("TORM4").Id,
                10,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            application?.GetInventory("S").inventoryItems.FirstOrDefault(x => x.ProductCode == "TORM4").Quantity.Should().Be(10);
        }

        /// <summary>
        /// Modificamos la cantidad de un producto del almac�n en negativo, que debe de dar un error
        /// </summary>
        [Test]
        public void ModifyItemFromToInventory_QuantityIsNeghative_ShouldThrowError()
        {
            // ARRANGE
            var application = _serviceProvider.GetService<IInventoryApplication>();

            // ACT
            Action response=()=>application?.ModifyInventory(
                application.GetProductByCode("TORM4").Id,
                -10,
                application.GetWarehouseByCode("S").Id);

            // ASSERT
            var quantityBeforeAdding = application?.GetInventory("S").inventoryItems.FirstOrDefault(x => x.ProductCode == "TORM4").Quantity;
            response.Should().Throw<ValidationException>().WithMessage("*'Quantity'*mayor*igual*'0'*");
            var quantityBeforeAdding2 = application?.GetInventory("S").inventoryItems.FirstOrDefault(x => x.ProductCode == "TORM4").Quantity;
        }




    }
}