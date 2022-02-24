using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductType { get; set; }
        public String ProductCategory { get; set; }
        public int ProductQuantity { get; set; }
        public int VendorId { get; set; }

    }
}