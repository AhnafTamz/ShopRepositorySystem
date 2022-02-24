using CRUD_ADONet_jQuery_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_ADONet_jQuery_MVC.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Product";
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
                            products.Add(new ProductModel
                            {
                                ProductId = Convert.ToInt32(sdr["ProductId"]),
                                ProductName = Convert.ToString(sdr["Name"]),
                                ProductType = Convert.ToString(sdr["Type"]),
                                ProductCategory = Convert.ToString(sdr["Category"]),
                                ProductQuantity = Convert.ToInt32(sdr["Quantity"]),
                                VendorId = Convert.ToInt32(sdr["VendorId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (products.Count == 0)
            {
                return Json("No Product Found", JsonRequestBehavior.AllowGet);
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            List<CategoryModel> Categories = new List<CategoryModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Category";
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
                            Categories.Add(new CategoryModel
                            {
                                CategoryId = Convert.ToInt32(sdr["Id"]),
                                CategoryName = Convert.ToString(sdr["Name"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (Categories.Count == 0)
            {
                return Json("No Categories Found", JsonRequestBehavior.AllowGet);
            }
            return Json(Categories, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTypes()
        {
            List<TypeModel> Types = new List<TypeModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Type";
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
                            Types.Add(new TypeModel
                            {
                                TypeId = Convert.ToInt32(sdr["Id"]),
                                TypeName = Convert.ToString(sdr["Name"]),
                                CategoryId = Convert.ToInt32(sdr["CategoryId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (Types.Count == 0)
            {
                return Json("No Types Found", JsonRequestBehavior.AllowGet);
            }
            return Json(Types, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductbyId(int id)
        {
            List<ProductModel> products = new List<ProductModel>();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM Product where productId = " + id;
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
                            products.Add(new ProductModel
                            {
                                ProductId = Convert.ToInt32(sdr["ProductId"]),
                                ProductName = Convert.ToString(sdr["Name"]),
                                ProductType = Convert.ToString(sdr["Type"]),
                                ProductCategory = Convert.ToString(sdr["Category"]),
                                ProductQuantity = Convert.ToInt32(sdr["Quantity"]),
                                VendorId = Convert.ToInt32(sdr["VendorId"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            if (products.Count == 0)
            {
                return Json("Product Doesn't Exist", JsonRequestBehavior.AllowGet);
            }
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertProduct(ProductModel product)
        {
            string query = "INSERT INTO Product VALUES (@ProductId, @Name, @Type, @Category, @Quantity, @VendorId)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@Name", product.ProductName);
                    cmd.Parameters.AddWithValue("@Type", product.ProductType);
                    cmd.Parameters.AddWithValue("@Category", product.ProductCategory);
                    cmd.Parameters.AddWithValue("@Quantity", product.ProductQuantity);
                    cmd.Parameters.AddWithValue("@VendorId", product.VendorId);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Product Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertCategory(CategoryModel category)
        {
            string query = "INSERT INTO Category VALUES (@Id, @Name)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", category.CategoryId);
                    cmd.Parameters.AddWithValue("@Name", category.CategoryName);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Category Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertType(TypeModel type)
        {
            string query = "INSERT INTO Type VALUES (@Id, @Name, @CategoryId)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", type.TypeId);
                    cmd.Parameters.AddWithValue("@Name", type.TypeName);
                    cmd.Parameters.AddWithValue("@CategoryId", type.CategoryId);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Json("Type Successfully Inserted", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductModel product)
        {
            string query = "UPDATE Product SET Name=@ProductName, Type=@ProductType, Category=@ProductCategory, Quantity=@ProductQuantity WHERE ProductId=@ProductId";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Name", product.ProductName);
                    cmd.Parameters.AddWithValue("@Type", product.ProductType);
                    cmd.Parameters.AddWithValue("@Category", product.ProductCategory);
                    cmd.Parameters.AddWithValue("@Quantity", product.ProductQuantity);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Product Successfully Updated", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteProduct(int productId)
        {
            string query = "DELETE FROM Product WHERE productId=@productId";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteCategory(int cId)
        {
            string query = "DELETE FROM Category WHERE Id=@cId";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", cId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return Json("Successfully Deleted", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteType(int tId)
        {
            string query = "DELETE FROM Type WHERE Id=@Id";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Id", tId);
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