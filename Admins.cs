using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_shop
{
    public enum ResultAdmin { nulll,userDelete, addProduct, redactProduct,deleteProduct, off };
    public partial class AdminsForm : Form
    {
        public ResultAdmin ChoiceAdmin;

        //public string NameAdmin { get; set; }
        MenuStrip menuStrip;
        ToolStripMenuItem toolStripMenu1;
        AddProductForm addProduct;
        UsersControl userControl;
        public AdminsForm(BindingList<User> users1)
        {
            InitializeComponent();
            addProduct=new AddProductForm( );
            userControl = new UsersControl(users1);
            this.IsMdiContainer = true;
            addProduct.MdiParent= this;
            userControl.MdiParent = this;
        }

        private void AdminsForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            this.MaximizeBox= false;
            menuStrip = new MenuStrip();
            toolStripMenu1 = new ToolStripMenuItem("Формы");
            toolStripMenu1.Dock = DockStyle.Top;
            toolStripMenu1.DropDownItems.Add("Добавить",null,ToolItemAdd_Click);
            toolStripMenu1.DropDownItems.Add("Управление Users",null,ToolItemControlUsers_Click);


            menuStrip.Items.Add(toolStripMenu1);
            Controls.Add(menuStrip);
            
        }

        private void ToolItemControlUsers_Click(object? sender ,EventArgs e)
        {
            userControl.Visible=true;
            userControl.Activate();
        }
        private void ToolItemAdd_Click(object? sender, EventArgs e)
        {
            addProduct.Visible=true;
            addProduct.Activate();
            ChoiceAdmin = ResultAdmin.addProduct;
        }
    }
}
