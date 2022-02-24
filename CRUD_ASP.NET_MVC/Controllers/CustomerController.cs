using CRUD_ADONet_jQuery_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_ADONet_jQuery_MVC.Controllers
{
    public class CustomerController : Controller
    {
        [HttpGet]
        public ActionResult GetCustomers()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Customer";
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
                            customers.Add(new CustomerModel
                            {
                                CustomerId = Convert.ToInt32(sdr["Id"]),
                                CustomerName = Convert.ToString(sdr["Name"]),
                                Address = Convert.ToString(sdr["Address"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (customers.Count == 0)
            {
                return Json("No Customer Found", JsonRequestBehavior.AllowGet);
            }
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCustomerOrders()
        {
            List<CustomerOrderModel> orders = new List<CustomerOrderModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM CustomerOrder";
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
                            orders.Add(new CustomerOrderModel
                            {
                                OrderId = Convert.ToInt32(sdr["Id"]),
                                CustomerId = Convert.ToInt32(sdr["CustomerId"]),
                                ShopId = Convert.ToInt32(sdr["ShopId"]),
                                ProductId = Convert.ToInt32(sdr["ProductId"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                                Price = Convert.ToInt32(sdr["Quantity"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (orders.Count == 0)
            {
                return Json("No Order Found", JsonRequestBehavior.AllowGet);
            }
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrderInvoice()
        {
            List<OrderInvoiceModel> orders = new List<OrderInvoiceModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM OrderInvoice";
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
                            orders.Add(new OrderInvoiceModel
                            {
                                InvoiceNo = Convert.ToInt32(sdr["InvoiceNo"]),
                                Date = Convert.ToDateTime(sdr["Date"]),
                                TotalPrice = Convert.ToInt32(sdr["TotalPrice"])
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
        public ActionResult InsertCustomer(CustomerModel customer)
        {
            string query = "INSERT INTO Customer VALUES (@Id, @Name, @Address)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Customer Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult InsertOrderInvoice(OrderInvoiceModel order)
        //{
        //    string query = "INSERT INTO OrderInvoice VALUES (@InvoiceNo, @Date, @TotalPrice)";
        //    string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(constr))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(query))
        //        {
        //            cmd.Parameters.AddWithValue("@InvoiceNo", order.InvoiceNo);
        //            cmd.Parameters.AddWithValue("@Date", order.Date);

        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //    }
        //    return Json("Invoice Successfully Inserted", JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult InsertCustomerOrder(OrderInvoiceModel corder)
        {
            int x = 0, y = 0;
            string query = "INSERT INTO CustomerOrder VALUES (@Id, @CustomerId, @ShopId, @ProductId, @Quantity, @Price, @InvoiceNo)";
            string query2 = "INSERT INTO OrderInvoice VALUES (@InvoiceNo, @Date, @TotalPrice)";
            string queryA2 = "Update ShopA SET Quantity=@Quantity WHERE P_Id=@pId";
            string queryB2 = "Update ShopB SET Quantity=@Quantity WHERE P_Id=@pId";
            string queryC2 = "Update ShopC SET Quantity=@Quantity WHERE P_Id=@pId";
            string queryA = "SELECT Quantity FROM ShopA WHERE P_Id=@PId";
            string queryB = "SELECT Quantity FROM ShopB WHERE P_Id=@PId";
            string queryC = "SELECT Quantity FROM ShopC WHERE P_Id=@PId";
            
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                if (corder.customerOrders[0].ShopId  == 1)
                {
                    int i = 0;
                    foreach (CustomerOrderModel order in corder.customerOrders)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryA))
                        {
                            cmd.Parameters.AddWithValue("@PId", corder.customerOrders[i].ProductId);
                            cmd.Connection = con;
                            con.Open();

                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    x = Convert.ToInt32(sdr["Quantity"]);
                                    if (x >= corder.customerOrders[i].Quantity)
                                    {
                                        y = x - corder.customerOrders[i].Quantity;
                                    }
                                    else
                                    { return Json("Product Exceeded Quantity in ShopA Repository", JsonRequestBehavior.AllowGet); }
                                }
                            }
                            con.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand(queryA2))

                        {
                            cmd.Parameters.AddWithValue("@pId", corder.customerOrders[i].ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", y);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        x = 0; y = 0; i++;
                    }
                    i = 0;
                }

                else if (corder.customerOrders[0].ShopId == 2)
                {
                    int i = 0;
                    foreach (CustomerOrderModel order in corder.customerOrders)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryB))
                        {
                            cmd.Parameters.AddWithValue("@PId", corder.customerOrders[i].ProductId);
                            cmd.Connection = con;
                            con.Open();

                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    x = Convert.ToInt32(sdr["Quantity"]);
                                    if (x >= corder.customerOrders[i].Quantity)
                                    {
                                        y = x - corder.customerOrders[i].Quantity;
                                    }
                                    else
                                    { return Json("Product Exceeded Quantity in ShopB Repository", JsonRequestBehavior.AllowGet); }
                                }
                            }
                            con.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand(queryB2))

                        {
                            cmd.Parameters.AddWithValue("@pId", corder.customerOrders[i].ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", y);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        x = 0; y = 0; i++;
                    }
                    i = 0;
                }

                else if (corder.customerOrders[0].ShopId == 3)
                {
                    int i = 0;
                    foreach (CustomerOrderModel order in corder.customerOrders)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryC))
                        {
                            cmd.Parameters.AddWithValue("@PId", corder.customerOrders[i].ProductId);
                            cmd.Connection = con;
                            con.Open();

                            using (SqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read())
                                {
                                    x = Convert.ToInt32(sdr["Quantity"]);
                                    if (x >= corder.customerOrders[i].Quantity)
                                    {
                                        y = x - corder.customerOrders[i].Quantity;
                                    }
                                    else
                                    { return Json("Product Exceeded Quantity in ShopC Repository", JsonRequestBehavior.AllowGet); }
                                }
                            }
                            con.Close();
                        }

                        using (SqlCommand cmd = new SqlCommand(queryC2))

                        {
                            cmd.Parameters.AddWithValue("@pId", corder.customerOrders[i].ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", y);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        x = 0; y = 0; i++;
                    }
                    i = 0;
                }
                else { return Json("Shop Doesn't Exist", JsonRequestBehavior.AllowGet); }

                using (SqlCommand cmd = new SqlCommand(query2))
                {
                    cmd.Parameters.AddWithValue("@InvoiceNo", corder.InvoiceNo);
                    cmd.Parameters.AddWithValue("@Date", corder.Date);
                    cmd.Parameters.AddWithValue("@TotalPrice", corder.TotalPrice);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                int j = 0;
                foreach (CustomerOrderModel order in corder.customerOrders)
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                    
                        cmd.Parameters.AddWithValue("@Id", corder.customerOrders[j].OrderId);
                        cmd.Parameters.AddWithValue("@CustomerId", corder.customerOrders[j].CustomerId);
                        cmd.Parameters.AddWithValue("@ShopId", corder.customerOrders[j].ShopId); 
                        cmd.Parameters.AddWithValue("@ProductId", corder.customerOrders[j].ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", corder.customerOrders[j].Quantity);
                        cmd.Parameters.AddWithValue("@Price", corder.customerOrders[j].Price);
                        cmd.Parameters.AddWithValue("@InvoiceNo", corder.customerOrders[j].InvoiceNo);

                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        j++;
                    }
                }
                j = 0;
 
            }
            x = 0; y = 0;

            return Json("Order Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteCustomer(int cid)
        {
            string query = "DELETE FROM Customer WHERE Id=@cid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@cid", cid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteCustomerOrder(int id)
        {
            string query = "DELETE FROM CustomerOrder WHERE Id=@oid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@oid", id);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteOrderInvoice(int oid)
        {
            string query = "DELETE FROM OrderInvoice WHERE InvoiceNo=@id";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@id", oid);
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