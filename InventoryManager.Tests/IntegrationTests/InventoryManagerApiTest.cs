using FluentAssertions;
using IdentityModel.Client;
using InventoryManager.Application.Views;
using InventoryManager.Domain;
using InventoryManager.Infraestructure;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Xml.Xsl;

namespace InventoryManager.Tests.IntegrationTests
{
    /// <summary>
    /// Tests de integración a nivel de API, probaremos que el API esté protegida con autenticación
    /// </summary>
    public class InventoryManagerApiTest
    {
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Probamos sin loguearnos, debe de darnos un 401
        /// </summary>
        [Test]
        public async Task GetAllInventory_WithoutAutentication_ShouldReturn401()
        {
            // ARRANGE

            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            // ACT
            var response = await client.GetAsync("/Inventory/GetInventory");

            // ASSERT
            var data = response.Content.ReadFromJsonAsync<InventoryItemListView>();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        }

        /// <summary>
        /// Probamos con el login correcto, debe de acceder
        /// </summary>
        [Test]
        public async Task GetAllInventory_WithAuthentication_ShouldReturnData()
        {
            // ARRANGE
            var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();
            client.SetBasicAuthentication("admin", "admin");

            // ACT
            var response = await client.GetAsync("/Inventory/GetInventory");

            // ASSERT
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var data = await response.Content.ReadFromJsonAsync<InventoryItemListView>();
            data.Should().NotBeNull();
            data.inventoryItems.Should().HaveCountGreaterThan(0);

        }

        /// <summary>
        /// Probamos con login incorrecto, debe de dar 401
        /// </summary>
        [Test]
        public async Task GetAllInventory_WithWrongAuthentication_ShouldReturn401()
        {
            // ARRANGE
            var application = new WebApplicationFactory<Program>();

            var client = application.CreateClient();
            client.SetBasicAuthentication("admin", "1234");
            // ACT
            var response = await client.GetAsync("/Inventory/GetInventory");

            // ASSERT
            var data = response.Content.ReadFromJsonAsync<InventoryItemListView>();
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}