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
    public partial class UsersControl : Form
    {
        public DataGridView dataGrid;
        BindingList<User> users;
        public UsersControl(BindingList<User>users)
        {
            InitializeComponent();
            this.users = users;
        }

        private void UsersControl_Load(object sender, EventArgs e)
        {
            dataGrid=new DataGridView();
            dataGrid.DataSource = users;
            dataGrid.Location = new Point(20, 20);
            dataGrid.Width= 600;
            dataGrid.ReadOnly = false;
            /*dataGrid.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;*/
            Controls.Add(dataGrid);
            this.FormClosing += UsersControl_FormClosing;
        }

        private void UsersControl_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel=true;
            this.Visible= false;
        }
    }
}
