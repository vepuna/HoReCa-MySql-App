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
        public string id, name, quantity;

        public FormInfo(FormStudent parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        public void SaveInfo()
        {
            lbltext.Text = "Добавить Продукты";
            btnSave.Text = "Save";
        }

        public void UpdateInfo()
        {
            lbltext.Text = "Update";
            btnSave.Text = "Update";
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
            if (btnSave.Text == "Save")
            {
                products std = new products (txtName.Text.Trim(), txtQuantity.Text.Trim());
                DbStudent.AddStudent(std);
                Clear();
            }
            if(btnSave.Text == "Update")
            {
                products std = new products(txtName.Text.Trim(), txtQuantity.Text.Trim());
                DbStudent.UpdateStudent(std, id);
            }
            _parent.Display();

        }
        
    }
}
