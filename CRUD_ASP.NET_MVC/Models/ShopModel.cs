using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class ShopModel
    {
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String Type { get; set; }
        public String Category { get; set; }
        public int Quantity { get; set; }
    }
}