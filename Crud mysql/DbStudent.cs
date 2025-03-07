﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_mysql
{
    internal class DbStudent
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "server=127.0.0.1;port=3306;user=root;password=root;database=pizzeria;";

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

        public static void AddStudent(Products std)
        {
            string sql = "INSERT INTO Products VALUES (NULL, @ProductName, @ProductQuantity)";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = std.Name;
            cmd.Parameters.Add("@ProductQuantity", MySqlDbType.VarChar).Value = std.Quantity;

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

        public static void UpdateStudent(Products std, string id)
        {
            string sql = "UPDATE Products SET Name = @ProductName, Quantity = @ProductQuantity  WHERE ProductID = @ProductsID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@ProductName", MySqlDbType.VarChar).Value = std.Name;
            cmd.Parameters.Add("@ProductQuantity", MySqlDbType.VarChar).Value = std.Quantity;
            cmd.Parameters.Add("@ProductsID", MySqlDbType.VarChar).Value = id;
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
            string sql = "DELETE FROM Products WHERE ProductID = @ProductsID";
            MySqlConnection connection = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@ProductsID", MySqlDbType.VarChar).Value = id;

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