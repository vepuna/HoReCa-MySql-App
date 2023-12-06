using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_mysql
{
    internal class TbOrder
    {
        public static string connectionString = "datasource=localhost;port=3306;username=root;password=;database=pizzeria";
        public static void LoadAvailableDishes(DataGridView tableOrder)
        {


            string query = "SELECT d.DishID, " +
                "d.DishName, " +
                "d.Price, " +
                "FLOOR(MIN(p.Quantity / di.QuantityRequired)) as MaxAvailable " +
                "FROM DishIngredients di " +
                "JOIN Dishes d ON di.DishID = d.DishID " +
                "JOIN Products p ON di.ProductID = p.ProductID " +
                "GROUP BY d.DishID, d.DishName;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rowIndex = tableOrder.Rows.Add();
                        tableOrder.Rows[rowIndex].Cells["DishID"].Value = reader["DishID"];
                        tableOrder.Rows[rowIndex].Cells["Dish"].Value = reader["DishName"];
                        tableOrder.Rows[rowIndex].Cells["Price"].Value = reader["Price"];
                        tableOrder.Rows[rowIndex].Cells["AvailableDish"].Value = reader["MaxAvailable"];
                    }
                }
            }
        }

        private static void ProcessOrder(object dishID, object requiredQuantity)

        {
            int dishId = Convert.ToInt32(dishID);
            int quantity = Convert.ToInt32(requiredQuantity);
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var productsRequired = GetProductsRequiredForDish(dishId, quantity);

                foreach (var product in productsRequired)
                {
                    int productID = product.Key;
                    int quantityRequired = product.Value;

                    string query = "UPDATE Products SET Quantity = Quantity - @QuantityRequired WHERE ProductID = @ProductID";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@QuantityRequired", quantityRequired);
                        command.Parameters.AddWithValue("@ProductID", productID);
                        command.ExecuteNonQuery();
                    }
                }

            }
        }
        private static Dictionary<int, int> GetProductsRequiredForDish(int dishID, int dishQuantity)
        {
            var productsRequired = new Dictionary<int, int>();
            string query = @"
                            SELECT ProductID, QuantityRequired 
                            FROM DishIngredients 
                            WHERE DishID = @DishID";

            using (var connection = new MySqlConnection(connectionString))
            {
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@DishID", dishID);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int productID = reader.GetInt32("ProductID");
                        int quantityRequired = reader.GetInt32("QuantityRequired");
                        quantityRequired *= dishQuantity;

                        if (productsRequired.ContainsKey(productID))
                        {
                            productsRequired[productID] += quantityRequired;
                        }
                        else
                        {
                            productsRequired.Add(productID, quantityRequired);
                        }
                    }
                }
            }

            return productsRequired;
        }

        public static void SaveOrder (DataGridView tableOrder, TextBox textBox1, ComboBox comboBox)
        {
            var selectedDish = (dynamic)comboBox.SelectedItem;
            int EmployeeID = selectedDish.Value;

            string customerName = textBox1.Text;
            DateTime orderDate = DateTime.Now;

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (DataGridViewRow row in tableOrder.Rows)
                {
                    if (row.Cells["DishID"].Value != null && row.Cells["RequiredDish"].Value != null)
                    {
                        int dishID = Convert.ToInt32(row.Cells["DishID"].Value);
                        int orderQuantity = Convert.ToInt32(row.Cells["RequiredDish"].Value);
                        int availableQuantity = Convert.ToInt32(row.Cells["AvailableDish"].Value);

                        ProcessOrder(row.Cells["DishID"].Value, row.Cells["RequiredDish"].Value);

                        if (orderQuantity > 0 && orderQuantity <= availableQuantity)
                        {
                            decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                            decimal totalPrice = orderQuantity * price;

                            string query = "INSERT INTO Orders (OrderDate, CustomerName, DishID, Quantity, TotalPrice) VALUES (@OrderDate, @CustomerName, @DishID, @Quantity, @TotalPrice)";
                            using (var command = new MySqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@OrderDate", orderDate);
                                command.Parameters.AddWithValue("@CustomerName", customerName);
                                command.Parameters.AddWithValue("@DishID", dishID);
                                command.Parameters.AddWithValue("@Quantity", orderQuantity);
                                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                                command.ExecuteNonQuery();
                            }
                            string queryEmployee = "INSERT INTO employeesalaries (EmployeeID, CommissionAmount) VALUES (@EmployeeID, @CommissionAmount);";
                            using (var command = new MySqlCommand(queryEmployee, connection))
                            {


                                command.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                                command.Parameters.AddWithValue("@CommissionAmount", totalPrice / 10);
                                command.ExecuteNonQuery();

                            }
                        }
                        else
                        {
                            MessageBox.Show($"Заказанное количество для блюда {row.Cells["Dish"].Value} превышает доступное.");
                            return;
                        }
                    }

                }
                connection.Close();
            }
            tableOrder.Rows.Clear();
            LoadAvailableDishes(tableOrder);
        }

        public static void LoadEmployee(ComboBox comboBox1)
        {
            comboBox1.Items.Clear();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT EmployeeID, EmployeeName FROM employees";
                var command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(new
                        {
                            Text = reader["EmployeeName"].ToString(),
                            Value = reader.GetInt32("EmployeeID")
                        });
                    }
                }
                connection.Close();
            }
        }
        public static void LoadReport(DataGridView tableOrder)
        {
            string query = "SELECT Orders.OrderID, Orders.OrderDate, Orders.CustomerName, Dishes.DishName, Orders.Quantity, Orders.TotalPrice FROM Orders JOIN Dishes ON Orders.DishID = Dishes.DishID;";
            using (var connection = new MySqlConnection(connectionString))
            {
                var command = new MySqlCommand(query, connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int rowIndex = tableOrder.Rows.Add();
                        tableOrder.Rows[rowIndex].Cells["reportOrderID"].Value = reader["OrderID"];
                        tableOrder.Rows[rowIndex].Cells["reportOrderDate"].Value = reader["OrderDate"];
                        tableOrder.Rows[rowIndex].Cells["reportCustomerName"].Value = reader["CustomerName"];
                        tableOrder.Rows[rowIndex].Cells["reportDish"].Value = reader["DishName"];
                        tableOrder.Rows[rowIndex].Cells["reportQuantity"].Value = reader["Quantity"];
                        tableOrder.Rows[rowIndex].Cells["reportTotalPrice"].Value = reader["TotalPrice"];
                    }
                }
            }


        }
        
    }
}
