using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DemoCRPushModel2.Repository;
using DemoCRPushModel2.Report;

namespace DemoCRPushModel2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LoadReportProduct();
        }

        private void LoadReportProduct()
        {
            IProductRepository productRepo = new ProductRepository();
            var listOfProduct = productRepo.GetAll();

            var listOfCategory = listOfProduct.Select(f => f.Category)
                                              .GroupBy(gb => gb.CategoryID).Select(g => g.First()).ToList();

            var listOfSupplier = listOfProduct.Select(f => f.Supplier)
                                              .GroupBy(gb => gb.SupplierID).Select(g => g.First()).ToList();

            var reportProduct = new CrProduct();

            // set data source report
            reportProduct.Database.Tables["Supplier"].SetDataSource(listOfSupplier);
            reportProduct.Database.Tables["Category"].SetDataSource(listOfCategory);
            reportProduct.Database.Tables["Product"].SetDataSource(listOfProduct);

            crystalReportViewer1.ReportSource = reportProduct;
        }
    }
}
