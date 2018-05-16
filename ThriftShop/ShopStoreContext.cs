using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThriftShop.Models;
using Microsoft.AspNetCore.Http;
using Ado;

namespace ThriftShop
{
    public class ShopStoreContext
    {

        public string ConnectionString { get; set; }

        public ShopStoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public void getDataSet()
        {
           // DataSet dataset = new DataSet();
        }


        public List<Product> GetAllProducts()
        {
            List<Product> list = new List<Product>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Category = reader["Category"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            BrandId = Convert.ToInt32(reader["Brand_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public int AddProductIntoDB(Product product)
        {
            try
            {
                using (var context = new DataContext(ConnectionString))
                {
                    var productId = context.ExecuteGetIdentity("INSERT INTO product (Name, Price, Category,Brand_Id ) VALUES (@0,@1,@2,@3)",product.Name,product.Price,product.Category,product.BrandId);
                    //If you want commit now, call method below, else, you could call later.
                    context.Commit();

                    return productId;
                }
            }
            catch (Exception e)
            {
                String msg = e.Message;

            }

            return -1;
        }


        public List<Brand> GetAllBrands()
        {
            List<Brand> list = new List<Brand>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Brand ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Brand()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            BrandName = reader["BrandName"].ToString()
                        });
                    }
                }
            }
            return list;
        }


        public List<FullProduct> GetAllFullProducts()
        {
            List<FullProduct> list = new List<FullProduct>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product ", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new FullProduct()
                        {
                            product = new Product()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Category = reader["Category"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"]),
                                BrandId = Convert.ToInt32(reader["Brand_Id"])
                            },
                            brand = new Brand()
                            {
                               
                            }
                        });
                    }
                }
            }
            return list;
        }

    }
}
