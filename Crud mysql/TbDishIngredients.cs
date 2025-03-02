using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_mysql
{
    internal class TbDishIngredients
    {
        public static string connectionString = "server=127.0.0.1;port=3306;user=root;password=root;database=pizzeria;";

        public static void LoadDishes(ComboBox comboBox1)
        {
            comboBox1.Items.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DishID, DishName FROM Dishes";
                var command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(new
                        {
                            Text = reader["DishName"].ToString(),
                            Value = reader.GetInt32("DishID")
                        });
                    }
                }
                connection.Close();
            }
        }

        private static List<Product> GetProductsFromDatabase()
        {
            List<Product> products = new List<Product>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ProductID, Name, Quantity FROM Products";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductID = reader.GetInt32("ProductID"),
                                Name = reader.GetString("Name"),
                                Quantity = reader.GetDecimal("Quantity")
                            });
                        }
                    }
                    connection.Close();
                }
            }

            return products;
        }

        public static void LoadDataIntoDataGridView(DataGridView dataGridView1)
        {
            var products = GetProductsFromDatabase();
            dataGridView1.Rows.Clear();
            foreach (var product in products)
            {
                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIndex].Cells["ProductID"].Value = product.ProductID;
                dataGridView1.Rows[rowIndex].Cells["Product"].Value = product.Name;
                dataGridView1.Rows[rowIndex].Cells["Available"].Value = product.Quantity;
            }
        }

        public static void SaveDishIngredients(ComboBox comboBox, DataGridView dataGridView)
        {
            var selectedDish = (dynamic)comboBox.SelectedItem;
            int dishID = selectedDish.Value;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells["ProductID"].Value != null && row.Cells["Required"].Value != null)
                {
                    int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    int productRequired = Convert.ToInt32(row.Cells["Required"].Value);

                    if (productRequired >= 1)
                    {
                        using (var connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string query = @"INSERT INTO DishIngredients (DishID, ProductID, QuantityRequired)
                                             VALUES (@DishID, @ProductID, @QuantityRequired)
                                             ON DUPLICATE KEY UPDATE QuantityRequired = @QuantityRequired;";

                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@DishID", dishID);
                                command.Parameters.AddWithValue("@ProductID", productId);
                                command.Parameters.AddWithValue("@QuantityRequired", productRequired);

                                command.ExecuteNonQuery();
                            }
                            connection.Close();
                        }
                    }
                }
            }
        }
    }
}