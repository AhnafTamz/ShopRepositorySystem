using CRUD_ADONet_jQuery_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_ADONet_jQuery_MVC.Controllers
{
    public class ShopController : Controller
    {
        [HttpGet]
        public ActionResult GetShops()
        {
            List<ShopInfoModel> shops = new List<ShopInfoModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Shop";
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
                            shops.Add(new ShopInfoModel
                            {
                                ShopId = Convert.ToInt32(sdr["Id"]),
                                ShopName = Convert.ToString(sdr["Name"]),
                                Location = Convert.ToString(sdr["Location"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (shops.Count == 0)
            {
                return Json("No Shop Found", JsonRequestBehavior.AllowGet);
            }
            return Json(shops, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetShopA()
        {
            List<ShopModel> shops = new List<ShopModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM ShopA";
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
                            shops.Add(new ShopModel
                            {
                                ProductId = Convert.ToInt32(sdr["P_Id"]),
                                ProductName = Convert.ToString(sdr["P_Name"]),
                                Type = Convert.ToString(sdr["P_Type"]),
                                Category = Convert.ToString(sdr["P_Category"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (shops.Count == 0)
            {
                return Json("No Product Found", JsonRequestBehavior.AllowGet);
            }
            return Json(shops, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetShopB()
        {
            List<ShopModel> shops = new List<ShopModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM ShopB";
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
                            shops.Add(new ShopModel
                            {
                                ProductId = Convert.ToInt32(sdr["P_Id"]),
                                ProductName = Convert.ToString(sdr["P_Name"]),
                                Type = Convert.ToString(sdr["P_Type"]),
                                Category = Convert.ToString(sdr["P_Category"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (shops.Count == 0)
            {
                return Json("No Product Found", JsonRequestBehavior.AllowGet);
            }
            return Json(shops, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetShopC()
        {
            List<ShopModel> shops = new List<ShopModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM ShopC";
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
                            shops.Add(new ShopModel
                            {
                                ProductId = Convert.ToInt32(sdr["P_Id"]),
                                ProductName = Convert.ToString(sdr["P_Name"]),
                                Type = Convert.ToString(sdr["P_Type"]),
                                Category = Convert.ToString(sdr["P_Category"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (shops.Count == 0)
            {
                return Json("No Product Found", JsonRequestBehavior.AllowGet);
            }
            return Json(shops, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetShopOrders()
        {
            List<ShopOrderModel> sorders = new List<ShopOrderModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM ShopOrder";
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
                            sorders.Add(new ShopOrderModel
                            {
                                OrderId = Convert.ToInt32(sdr["Id"]),
                                ShopId = Convert.ToInt32(sdr["S_Id"]),
                                ProductId = Convert.ToInt32(sdr["P_Id"]),
                                Quantity = Convert.ToInt32(sdr["Quantity"]),
                                Date = Convert.ToDateTime(sdr["Date"]),
                                Price = Convert.ToInt32(sdr["Price"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (sorders.Count == 0)
            {
                return Json("No Order Found", JsonRequestBehavior.AllowGet);
            }
            return Json(sorders, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertShop(ShopInfoModel shop)
        {
            string query = "INSERT INTO Shop VALUES (@Id, @Name, @Location)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", shop.ShopId);
                    cmd.Parameters.AddWithValue("@Name", shop.ShopName);
                    cmd.Parameters.AddWithValue("@Location", shop.Location);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Shop Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertShopA(ShopModel shop)
        {
            string query = "INSERT INTO ShopA VALUES (@P_Id, @P_Name, @P_Type, @P_Category, @Quantity)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@P_Id", shop.ProductId);
                    cmd.Parameters.AddWithValue("@P_Name", shop.ProductName);
                    cmd.Parameters.AddWithValue("@P_Type", shop.Type);
                    cmd.Parameters.AddWithValue("@P_Category", shop.Category);
                    cmd.Parameters.AddWithValue("@Quantity", shop.Quantity);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Product Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertShopB(ShopModel shop)
        {
            string query = "INSERT INTO ShopB VALUES (@P_Id, @P_Name, @P_Type, @P_Category, @Quantity)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@P_Id", shop.ProductId);
                    cmd.Parameters.AddWithValue("@P_Name", shop.ProductName);
                    cmd.Parameters.AddWithValue("@P_Type", shop.Type);
                    cmd.Parameters.AddWithValue("@P_Category", shop.Category);
                    cmd.Parameters.AddWithValue("@Quantity", shop.Quantity);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Product Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertShopC(ShopModel shop)
        {
            string query = "INSERT INTO ShopC VALUES (@P_Id, @P_Name, @P_Type, @P_Category, @Quantity)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@P_Id", shop.ProductId);
                    cmd.Parameters.AddWithValue("@P_Name", shop.ProductName);
                    cmd.Parameters.AddWithValue("@P_Type", shop.Type);
                    cmd.Parameters.AddWithValue("@P_Category", shop.Category);
                    cmd.Parameters.AddWithValue("@Quantity", shop.Quantity);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Product Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertShopOrder(ShopOrderModel sorder)
        {
            string Name = "";
            int x = 0, y = 0, a=0, b=0, c=0, d=0, e=0, f=0;
            string query = "INSERT INTO ShopOrder VALUES (@Id, @S_Id, @P_Id, @Quantity, @Date, @Price)";
            string query1A = "SELECT Name FROM Product WHERE ProductId=@PId";
            //string query1B = "SELECT Name FROM Product WHERE Name=@name";
            string query1 = "SELECT Quantity FROM Product WHERE Name=@pname";
            string query2 = "Update Product SET Quantity=@Quantity WHERE Name=@name";
            string queryA3 = "SELECT Quantity FROM ShopA WHERE P_Id=@PId";
            string queryB3 = "SELECT Quantity FROM ShopB WHERE P_Id=@PId";
            string queryC3 = "SELECT Quantity FROM ShopC WHERE P_Id=@PId";
            string queryA2 = "Update ShopA SET Quantity=@Quantity WHERE P_Id=@pId";
            string queryB2 = "Update ShopB SET Quantity=@Quantity WHERE P_Id=@pId";
            string queryC2 = "Update ShopC SET Quantity=@Quantity WHERE P_Id=@pId";

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query1A))
                {
                    cmd.Parameters.AddWithValue("@PId", sorder.ProductId);
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            Name = Convert.ToString(sdr["Name"]);
                        }
                    }
                    con.Close();
                }
                //using (SqlCommand cmd = new SqlCommand(query1B))
                //{
                //    cmd.Parameters.AddWithValue("@name", Name);
                //    cmd.Connection = con;
                //    con.Open();

                //    using (SqlDataReader sdr = cmd.ExecuteReader())
                //    {
                //        while (sdr.Read())
                //        {
                //            Name = Convert.ToString(sdr["P_Name"]);
                //        }
                //    }
                //    con.Close();
                //}
                using (SqlCommand cmd = new SqlCommand(query1))
                {
                    cmd.Parameters.AddWithValue("@pname", Name);
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            x = Convert.ToInt32(sdr["Quantity"]);
                            y = x - sorder.Quantity;
                        }
                    }
                    con.Close();
                }

                if(x==0)
                {
                    y = 0;
                    return Json("Product Doesn't Exist in Product Repository", JsonRequestBehavior.AllowGet);
                }

                if (sorder.ShopId == 1)
                {
                    using (SqlCommand cmd = new SqlCommand(queryA3))
                    {
                        cmd.Parameters.AddWithValue("@PId", sorder.ProductId);
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                a = Convert.ToInt32(sdr["Quantity"]);
                                b = sorder.Quantity + a;
                            }
                        }
                        con.Close();
                    }
                    if (a != null)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryA2))
                        {
                            cmd.Parameters.AddWithValue("@pId", sorder.ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", b);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            a = 0; b = 0;
                        }
                    }
                    else
                    {
                        a = 0; b = 0;
                        return Json("Product Doesn't Exist in Shop Repository", JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sorder.ShopId == 2)
                {
                    using (SqlCommand cmd = new SqlCommand(queryB3))
                    {
                        cmd.Parameters.AddWithValue("@PId", sorder.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", d);
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                c = Convert.ToInt32(sdr["Quantity"]);
                                d = sorder.Quantity + c;
                            }
                        }
                        con.Close();
                    }
                    if (c != null)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryB2))
                        {
                            cmd.Parameters.AddWithValue("@pId", sorder.ProductId);
                            cmd.Parameters.AddWithValue("@Quantity",d);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            c = 0; d = 0;
                        }
                    }
                    else
                    {
                        c = 0; d = 0;
                        return Json("Product Doesn't Exist in Shop Repository", JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sorder.ShopId == 3)
                {
                    using (SqlCommand cmd = new SqlCommand(queryC3))
                    {
                        cmd.Parameters.AddWithValue("@PId", sorder.ProductId);
                        cmd.Parameters.AddWithValue("@Quantity", f);
                        cmd.Connection = con;
                        con.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                e = Convert.ToInt32(sdr["Quantity"]);
                               f = sorder.Quantity + e;
                            }
                        }
                        con.Close();
                    }
                    if (e != null)
                    {
                        using (SqlCommand cmd = new SqlCommand(queryC2))
                        {
                            cmd.Parameters.AddWithValue("@pId", sorder.ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", f);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            e = 0; f = 0;
                        }
                    }
                    else
                    {
                        e = 0; f = 0;
                        return Json("Product Doesn't Exist in Shop Repository", JsonRequestBehavior.AllowGet);
                    }
                }
                else return Json("Invalid Shop ID", JsonRequestBehavior.AllowGet);

                using (SqlCommand cmd = new SqlCommand(query2))
                {
                    cmd.Parameters.AddWithValue("@name", Name);
                    cmd.Parameters.AddWithValue("@Quantity", y);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    x = 0; y = 0;
                }
               

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", sorder.OrderId);
                    cmd.Parameters.AddWithValue("@S_Id", sorder.ShopId);
                    cmd.Parameters.AddWithValue("@P_Id", sorder.ProductId);
                    cmd.Parameters.AddWithValue("@Quantity", sorder.Quantity);
                    cmd.Parameters.AddWithValue("@Date", sorder.Date);
                    cmd.Parameters.AddWithValue("@Price", sorder.Price);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Order Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteShop(int sid)
        {
            string query = "DELETE FROM Shop WHERE Id=@sid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", sid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteShopA(int pid)
        {
            string query = "DELETE FROM ShopA WHERE P_Id=@pid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@pid", pid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteShopB(int pid)
        {
            string query = "DELETE FROM ShopB WHERE P_Id=@pid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@pid", pid);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteShopC(int pid)
        {
            string query = "DELETE FROM ShopC WHERE P_Id=@pid";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@pid", pid);
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