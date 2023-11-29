using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud_mysql
{
    public partial class FormStudent : Form
    {
        FormInfo form;
        public FormStudent()
        {
            InitializeComponent();
            form = new FormInfo(this);
            // tabControl.ItemSize = new Size(0, 1);
            // tabControl.SizeMode = TabSizeMode.Fixed;
            Display();
        }

        
        public void Display()
        {
            DbStudent.DisplayAndSearch("SELECT ProductID, Name, Quantity FROM products", dataGridView);

        }
        public void DisplayMenu()
        {
            DbStudent.DisplayAndSearch("SELECT DishID, DishName, Price FROM dishes", dataGridViewMenu);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.SaveInfo();
            form.ShowDialog();
        }

        private void FormStudent_Shown(object sender, EventArgs e)
        {
            //Display();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DbStudent.DisplayAndSearch("SELECT ProductID, Name, Quantity FROM products WHERE Name LIKE'%" + textBox1.Text+"%'", dataGridView);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Edit user
            if(e.ColumnIndex == 0) {
                form.Clear();
                form.id = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.name = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.quantity = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.UpdateInfo();
                form.ShowDialog();
                return;
            }

            //Delete user
            if(e.ColumnIndex == 1)
            {
                if(MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes )
                {
                    DbStudent.DeleteStudent(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Display();
                }
                return;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtInfo_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnNewMenu_Click(object sender, EventArgs e)
        {
            form.Clear();
            form.SaveInfoMenu();
            form.ShowDialog();
            
        }

        /////////////////////////////////////////////
        ///menu 
        ////////////////////////////////////////////
             
        private void dataGridViewMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Edit user
            if (e.ColumnIndex == 0)
            {
                form.Clear();
                form.id = dataGridViewMenu.Rows[e.RowIndex].Cells[2].Value.ToString();
                form.name = dataGridViewMenu.Rows[e.RowIndex].Cells[3].Value.ToString();
                form.quantity = dataGridViewMenu.Rows[e.RowIndex].Cells[4].Value.ToString();
                form.UpdateInfoMenu();
                form.ShowDialog();
                return;
            }

            //Delete user
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    TbMenu.DeleteStudent(dataGridViewMenu.Rows[e.RowIndex].Cells[2].Value.ToString());
                    DisplayMenu();
                }
                return;
            }
            DisplayMenu();

        }
        

        private void dataGridViewMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            DisplayMenu();
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                Display();
            }
            if (e.TabPage == tabPage2)
            {
                DisplayMenu();
                
            }
        }

        private void dataGridViewMenu_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TbMenu.DisplayAndSearch("SELECT DishID, DishName, Price FROM dishes WHERE DishName LIKE'%" + textBox2.Text + "%'", dataGridViewMenu);
        }
    }
}
