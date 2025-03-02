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
    public partial class FormInfo : Form
    {
        private readonly FormStudent _parent;
        public string id, name, quantity, price;

        public FormInfo(FormStudent parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void SaveInfo()
        {
            lbltext.Text = "Добавить в Склад";
            btnSave.Text = "Сохранить";
        }

        public void UpdateInfo()
        {
            lbltext.Text = "Обновить Товар";
            btnSave.Text = "Update";
            txtName.Text = name;
            txtQuantity.Text = quantity;
        }
        /// <summary>
        /// Menu
        /// </summary>
        public void SaveInfoMenu()
        {
            lbltext.Text = "Сохранить в меню";
            btnSave.Text = "Сохранить";
            txtName.Text = name;
            txtQuantity.Text = quantity;
        }

        public void UpdateInfoMenu()
        {
            lbltext.Text = "Update in Menu";
            btnSave.Text = "Сохранить";
            txtName.Text = name;
            txtQuantity.Text = quantity;
        }


        public void Clear()
        {
            txtName.Text = txtQuantity.Text = string.Empty;
        }

        private void FormInfo_Load(object sender, EventArgs e)
        {

        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
       {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Product Name  < 3");
                return;
            }
            if (Convert.ToInt32(txtQuantity.Text.Trim()) < 1)
            {
                MessageBox.Show("Quantity reg  < 1");
                return;
            }


            //////
            if (lbltext.Text == "Добавить в Склад")
            {
                Products std = new Products (txtName.Text.Trim(), txtQuantity.Text.Trim());
                DbStudent.AddStudent(std);
                Clear();
            }
            if(lbltext.Text == "Обновить Товар")
            {
                Products std = new Products(txtName.Text.Trim(), txtQuantity.Text.Trim());
                DbStudent.UpdateStudent(std, id);
            }
            _parent.Display();


            //new menu
            if (lbltext.Text == "Сохранить в меню")
            {
                Menu std = new Menu(txtName.Text.Trim(), txtQuantity.Text.Trim());
                TbMenu.AddStudent(std);
                Clear();
            }

            //update menu
            if (lbltext.Text == "Update in Menu")
            {
                Menu std = new Menu(txtName.Text.Trim(), txtQuantity.Text.Trim());
                TbMenu.UpdateStudent(std, id);
                Clear();
            }
            _parent.DisplayMenu();

        }
        ///////////////////////////////////////////////////////////////////////////////////////////////
        
        
    }
}
