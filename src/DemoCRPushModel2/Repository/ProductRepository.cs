using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

using DemoCRPushModel2.Model;

namespace DemoCRPushModel2.Repository
{
    public interface IProductRepository
    {
        IList<Product> GetAll();
    }

    public class ProductRepository : IProductRepository
    {
        public IList<Product> GetAll()
        {
            IList<Product> listOfProduct = new List<Product>();

            using (IDapperContext context = new DapperContext())
            {
                var sql = @"SELECT Products.ProductID, Products.ProductName, Products.UnitPrice, Products.UnitsInStock, 
                            Categories.CategoryID, Categories.CategoryName, Categories.Description, 
                            Suppliers.SupplierID, Suppliers.CompanyName
                            FROM Categories INNER JOIN Products ON Categories.CategoryID = Products.CategoryID 
                            INNER JOIN Suppliers ON Products.SupplierID = Suppliers.SupplierID";
                listOfProduct = context.db.Query<Product, Category, Supplier, Product>(sql, (p, c, s) =>
                {
                    p.CategoryID = c.CategoryID; p.Category = c;
                    p.SupplierID = s.SupplierID; p.Supplier = s;

                    return p;
                }, splitOn: "CategoryID, SupplierID").ToList();          
            }

            return listOfProduct;
        }
    }
}
