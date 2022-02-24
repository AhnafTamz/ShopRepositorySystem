using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class DateQuantityModel
    {
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string T_Status { get; set; }
    }
}