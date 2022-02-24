using CRUD_ADONet_jQuery_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_ADONet_jQuery_MVC.Controllers
{
    public class VendorController : Controller
    {
        [HttpGet]
        public ActionResult GetVendors()
        {
            List<VendorModel> vendors = new List<VendorModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Vendor";
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
                            vendors.Add(new VendorModel
                            {
                                VendorId = Convert.ToInt32(sdr["Id"]),
                                VendorName = Convert.ToString(sdr["Name"]),
                                Product = Convert.ToString(sdr["Product"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (vendors.Count == 0)
            {
                return Json("No Vendor Found", JsonRequestBehavior.AllowGet);
            }
            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetVendorOrders()
        {
            List<VendorOrderModel> vorders = new List<VendorOrderModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM VendorOrder";
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
                            vorders.Add(new VendorOrderModel
                            {
                                OrderId = Convert.ToInt32(sdr["Id"]),
                                VendorId = Convert.ToInt32(sdr["V_Id"]),
                                ProductId = Convert.ToInt32(sdr["P_Id"]),
                                Quantity = Convert.ToInt32(sdr["quantity"]),
                                Date = Convert.ToDateTime(sdr["Date"]),
                                Price = Convert.ToInt32(sdr["Price"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (vorders.Count == 0)
            {
                return Json("No Order Found", JsonRequestBehavior.AllowGet);
            }
            return Json(vorders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertVendor(VendorModel vendor)
        {
            string query = "INSERT INTO Vendor VALUES (@Id, @Name, @Product)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", vendor.VendorId);
                    cmd.Parameters.AddWithValue("@Name", vendor.VendorName);
                    cmd.Parameters.AddWithValue("@Product", vendor.Product);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Vendor Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertVendorOrder(VendorOrderModel vorder)
        {
            int x=0,y=0;
            string query = "INSERT INTO VendorOrder VALUES (@Id, @V_Id, @P_Id, @Quantity, @Date, @Price)";
            string query2 = "Update Product SET Quantity=@Quantity WHERE ProductId=@pId";
            string query3 = "SELECT Quantity FROM Product WHERE ProductId=@PId";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query3))
                {
                    cmd.Parameters.AddWithValue("@PId", vorder.ProductId);
                    cmd.Connection = con;
                    con.Open();
                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            x = Convert.ToInt32(sdr["Quantity"]);
                            y = vorder.Quantity + x;
                        }
                    }
                    con.Close();
                }
                if (x >= 0)
                {
                    using (SqlCommand cmd = new SqlCommand(query2))
                    {
                        cmd.Parameters.AddWithValue("@pId", vorder.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", y);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else 
                {
                    return Json("Product Doesn't Exist in Product Repository", JsonRequestBehavior.AllowGet);
                }


                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", vorder.OrderId);
                    cmd.Parameters.AddWithValue("@V_Id", vorder.VendorId);
                    cmd.Parameters.AddWithValue("@P_Id", vorder.ProductId);
                    cmd.Parameters.AddWithValue("@Quantity", vorder.Quantity);
                    cmd.Parameters.AddWithValue("@Date", vorder.Date);
                    cmd.Parameters.AddWithValue("@Price", vorder.Price);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            x = 0; y = 0;

            return Json("Order Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteVendor(int vid)
        {
            string query = "DELETE FROM Vendor WHERE Id=@id";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", vid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteVendorOrder(int vid)
        {
            string query = "DELETE FROM VendorOrder WHERE Id=@vid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", vid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }
    }
}