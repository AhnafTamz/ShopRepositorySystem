using CRUD_ADONet_jQuery_MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_ADONet_jQuery_MVC.Controllers
{
    public class ReportController : Controller
    {
        [HttpGet]
        public ActionResult GetMonthlySaleReport()
        {
            List<MonthlySaleReportModel> orders = new List<MonthlySaleReportModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "select DATEPART(Month, Date) Month, SUM(TotalPrice) AS MonthlySUM from OrderInvoice group by DATEPART(Month, Date)";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            orders.Add(new MonthlySaleReportModel
                            {
                                Month = Convert.ToInt32(sdr["Month"]),
                                TotalPrice = Convert.ToInt32(sdr["MonthlySUM"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (orders.Count == 0)
            {
                return Json("No Invoices Found", JsonRequestBehavior.AllowGet);
            }
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetProductTransactionReport(DateProductModel dqModel)
        {
            int Q = 0;
            List<DateQuantityModel> Vorders = new List<DateQuantityModel>();
            List<PTReportModel> report = new List<PTReportModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT P_Id,Quantity,Date,Price,T_Status FROM VendorOrder WHERE P_Id=2008 AND Date between @date1 AND @date2 UNION SELECT P_Id,Quantity,Date,Price,T_Status FROM ShopOrder WHERE P_Id = @id AND Date between @date1 AND @date2 ORDER BY Date";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", dqModel.P_Id);
                    cmd.Parameters.AddWithValue("@date1", dqModel.StartDate);
                    cmd.Parameters.AddWithValue("@date2", dqModel.EndDate);

                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Vorders.Add(new DateQuantityModel
                            {
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                                Date = Convert.ToDateTime(sdr["Date"]),
                                T_Status = Convert.ToString(sdr["T_Status"])
                            });
                        }
                    }
                    con.Close();
                }
                int i = 0;
                foreach (DateQuantityModel order in Vorders)
                {
                    if (Vorders[i].T_Status.Trim() == "V")
                    {
                        report.Add(new PTReportModel
                        {
                            Date = Vorders[i].Date,
                            IniQuantity = Q,
                            P_Id = dqModel.P_Id,
                            In = Vorders[i].Quantity,
                            Out = 0,
                            Balance = Q + Vorders[i].Quantity
                        });
                        Q = Q + Vorders[i].Quantity;
                    }
                    else if (Vorders[i].T_Status.Trim() == "S")
                    {
                        report.Add(new PTReportModel
                        {
                            Date = Vorders[i].Date,
                            IniQuantity = Q,
                            P_Id = dqModel.P_Id,
                            In = 0,
                            Out = Vorders[i].Quantity,
                            Balance = Q - Vorders[i].Quantity
                        });
                        Q = Q - Vorders[i].Quantity;
                    }
                    i++;
                }
                i = 0;
                
                if (report.Count == 0)
                {
                    return Json("No Transactions Found", JsonRequestBehavior.AllowGet);
                }
                return Json(report, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
