using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class PTReportModel
    {
        public DateTime Date { get; set; }
        public int IniQuantity { get; set; }
        public int P_Id { get; set; }
        public int In { get; set; }
        public int Out { get; set; }
        public int Balance { get; set; }

    }
}