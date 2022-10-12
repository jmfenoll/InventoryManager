using InventoryManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infraestructure
{
    /// <summary>
    /// This class contains a database structure, like EntityFramework context.
    /// </summary>
    public class InventoryContext 
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Warehouse> Warehouses { get; set; } = new List<Warehouse>();



    }
}
