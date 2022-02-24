using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_ADONet_jQuery_MVC.Models
{
    public class TypeModel
    {
        public int TypeId { get; set; }
        public String TypeName { get; set; }
        public int CategoryId { get; set; }
    }
}