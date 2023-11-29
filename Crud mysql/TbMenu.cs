using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_mysql
{
    internal class TbMenu
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=pizzeria";
            MySqlConnection connection = new MySqlConnection(sql);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error with Connection", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return connection;
        }

        public static void AddStudent(menu std)
        {
            string sql = "INSERT INTO dishes VALUES (NULL, @DishesName, @DishesPrice)";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@DishesName", MySqlDbType.VarChar).Value = std.Name;
            cmd.Parameters.Add("@DishesPrice", MySqlDbType.VarChar).Value = std.Price;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student not insered.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void UpdateStudent(menu std, string id)
        {
            string sql = "UPDATE dishes SET DishName = @DishesName, Price = @DishesPrice  WHERE DishID = @DishID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@DishesName", MySqlDbType.VarChar).Value = std.Name;
            cmd.Parameters.Add("@DishesPrice", MySqlDbType.VarChar).Value = std.Price;
            cmd.Parameters.Add("@DishID", MySqlDbType.VarChar).Value = id;
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student not updated.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void DeleteStudent(string id)
        {
            string sql = "DELETE FROM dishes WHERE DishID = @DishID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@DishID", MySqlDbType.VarChar).Value = id;

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Delete Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Student not Deleted.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            string sql = query;
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);
            dgv.DataSource = tbl;
            connection.Close();
        }
    }
}
