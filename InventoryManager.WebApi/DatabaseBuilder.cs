using InventoryManager.CrossCutting.Exceptions;
using InventoryManager.Domain;
using InventoryManager.Infraestructure;

namespace InventoryManager.WebApi
{
    /// <summary>
    /// Extension methods to add authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class DatabaseBuilder
    {
        /// <summary>
        /// Adds the <see cref="AuthenticationMiddleware"/> to the specified <see cref="IApplicationBuilder"/>, which enables authentication capabilities.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static void LoadInitialData(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<InventoryContext>();
            if (context == null) throw new DIException("Can't find InventoryContext in DI");

            context.Products = new List<Product> {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="DEST123",
                    Name = "Destornillador Modelo 123",
                    Price = 3.54M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="TUEM5",
                    Name = "Tuerca M5",
                    Price = 0.02M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="TORM5",
                    Name = "Tornillo M5",
                    Price = 0.08M
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code="TUEM8",
                    Name = "Tuerca M8",
                    Price = 0.03M,
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "TORM8",
                    Name = "Tornillo M8",
                    Price = 0.15M
                } };

            context.Warehouses = new List<Warehouse>
            {
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
                            Product= context.Products.FirstOrDefault(p=>p.Code=="TORM8"),
                            Quantity=15
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= context.Products.FirstOrDefault(p=>p.Code=="TUEM8"),
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
                            Product= context.Products.FirstOrDefault(p=>p.Code=="TORM5"),
                            Quantity=15
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= context.Products.FirstOrDefault(p=>p.Code=="TUEM5"),
                            Quantity=12
                        },
                        new WarehouseItem
                        {
                            Id = Guid.NewGuid(),
                            Product= context.Products.FirstOrDefault(p=>p.Code=="DEST123"),
                            Quantity=8
                        },
                    }

                }
            };
        }
    }
}