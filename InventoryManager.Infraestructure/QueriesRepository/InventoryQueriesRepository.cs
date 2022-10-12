using InventoryManager.CrossCutting.Exceptions;
using InventoryManager.Domain;
using InventoryManager.Infraestructure;
using System.Diagnostics;
using System.Xml.Linq;

namespace InventoryManager.Infraestructure.QueriesRepository
{
    public class InventoryQueriesRepository : IInventoryQueriesRepository
    {
        private InventoryContext _context;

        public InventoryQueriesRepository(InventoryContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get list of WarehouseItems
        /// </summary>
        /// <param name="code">Warehouse code. If is null or empty, returns all warehouses.</param>
        /// <returns>A list of warehouse</returns>
        public IEnumerable<Warehouse> GetInventory(string? warehouseCode)
        {
            // Here we can use Dapper as database reader because of how ligth and fast it is

            // When we'll do database access, we must use async getters to improve performance


            // Here i use asQueriable for make a dynamic query and access once to bbdd on ToList command
            var query = _context.Warehouses.AsQueryable();

            if (!string.IsNullOrEmpty(warehouseCode))
                query = query.Where(w => w.Code == warehouseCode);

            return query.ToList();
        }

        /// <summary>
        /// Get product by code
        /// </summary>
        /// <param name="code">Code of product </param>
        /// <returns>a product</returns>
        /// <exception cref="DataNotFoundException"></exception>
        public Product GetProductByCode(string code)
        {
            var product=  _context.Products.FirstOrDefault(p => p.Code == code);
            if (product==null)
                throw new DataNotFoundException($"No se ha encontrado el producto con código {code}");
            return product;
        }

        /// <summary>
        /// Get warehouse by code
        /// </summary>
        /// <param name="code">Warehouse code</param>
        /// <returns>a warehouse</returns>
        /// <exception cref="DataNotFoundException"></exception>
        public Warehouse GetWarehouseByCode(string code)
        {
            var warehouse= _context.Warehouses.FirstOrDefault(p => p.Code == code);
            if (warehouse==null)
                throw new DataNotFoundException($"No se ha encontrado el almacén con código {code}");

            return warehouse;
            
        }


    }
}