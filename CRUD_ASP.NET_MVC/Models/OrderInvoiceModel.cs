using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class OrderInvoiceModel
    {
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public List<CustomerOrderModel> customerOrders { get; set; }

    }
}