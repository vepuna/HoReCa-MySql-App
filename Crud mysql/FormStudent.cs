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
        }

        
        public void Display()
        {
            DbStudent.DisplayAndSearch("SELECT ProductID, Name, Quantity FROM products", dataGridView);

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
            Display();
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
    }
}
