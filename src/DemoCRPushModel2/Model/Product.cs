using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoCRPushModel2.Model
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }

        public Category Category { get; set; }
        public int CategoryID { get; set; }

        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }        
    }
}
