using FluentAssertions.Common;
using InventoryManager.Application;
using InventoryManager.Domain;
using InventoryManager.Infraestructure.CommandsRepository;
using InventoryManager.Infraestructure.QueriesRepository;
using InventoryManager.Infraestructure;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManager.Tests.IntegrationTests
{
    public class CommonTest
    {
        protected ServiceCollection _services;
        protected ServiceProvider _serviceProvider;


        public CommonTest()
        {
            _services = new ServiceCollection();
        }

        /// <summary>
        /// Filling database context for tests
        /// </summary>
        protected void PrepareDatabase()
        {
            var _context = _serviceProvider.GetService<InventoryContext>();

            _context.Products = new List<Product> {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code= "DEST123",
                    Name = "Destornillador Modelo 123 Test",
                    Price = 3.54M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code= "TUEM2",
                    Name = "Tuerca M2 Test",
                    Price = 0.02M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code= "TORM2",
                    Name = "Tornillo M2 Test",
                    Price = 0.08M
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="TUEM4",
                    Name = "Tuerca M4 Test",
                    Price = 0.03M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="TORM4",
                    Name = "Tornillo M4 Test",
                    Price = 0.15M
                } };

            _context.Warehouses = new List<Warehouse> {
                new Warehouse
                {
                    Id = Guid.NewGuid(),
                    Name = "Zona Norte Test",
                    Code="N",
                    Items= new List<WarehouseItem>()
                    {
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= _context.Products.FirstOrDefault(p=>p.Code=="TORM2"),
                            Quantity=15
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= _context.Products.FirstOrDefault(p=>p.Code=="TUEM2"),
                            Quantity=12
                        },
                    }

                },
                new Warehouse
                {
                    Id = Guid.NewGuid(),
                    Code="S",
                    Name = "Zona Sur Test",
                    Items= new List<WarehouseItem>()
                    {
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= _context.Products.FirstOrDefault(p=>p.Code=="TORM4"),
                            Quantity=15
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= _context.Products.FirstOrDefault(p=>p.Code=="TUEM4"),
                            Quantity=12
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= _context.Products.FirstOrDefault(p=>p.Code=="DEST123"),
                            Quantity=8
                        },
                    }

                }};
        }

        /// <summary>
        /// Inject services in the Dependency Containers
        /// </summary>
        protected void PrepareDI()
        {
            _services.AddTransient<IInventoryQueriesRepository, InventoryQueriesRepository>();
            _services.AddTransient<IInventoryCommandsRepository, InventoryCommandsRepository>();
            _services.AddTransient<IInventoryApplication, InventoryApplication>();
            _services.AddSingleton<InventoryContext>();
            _serviceProvider = _services.BuildServiceProvider();

        }

    }
}