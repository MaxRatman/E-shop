using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_shop
{
    public partial class AddProductForm : Form
    {
        Button cancelButton;
        Button addImage;
        public Image image { get; set; }

        Button fullText;
        public string ?fullTextS { get; set; }
        public Button? add { get; set; }
        public TextBox? name { get; set; }
        public TextBox? id { get; set; }
        public ComboBox group;
        public Product product { get; set; }

        public TextBox? cell { get; set; }
        public AddProductForm()
        {
            InitializeComponent();
        }
        private void AddPrdouctForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(200, 160);
            this.MaximizeBox= false;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Activated += AddProductForm_Activated;
            //form1 = new Form1();

            cancelButton=new Button();
            cancelButton.Location = new Point(0, 60);
            cancelButton.Text = "Cancel";
            cancelButton.Size = new Size(100, 25);

            addImage=new Button();
            addImage.Text = "Add image";
            addImage.Click += AddImage_Click;
            addImage.Location = new Point(0, 0);
            addImage.Size = new Size(100, 20);

            fullText=new Button();
            fullText.Text = "Text";
            fullText.Location = new Point(0, 20);
            fullText.Size = new Size(100, 20);
            fullText.Click += FullText_Click;

            add=new Button();
            add.Text = "Add";
            add.Size=new Size(100, 20);
            add.Location = new Point(0, 40);
            add.Click += Add_Click;

            name=new TextBox();
            name.PlaceholderText="Name";
            name.Location = new Point(100, 0);

            id=new TextBox();
            id.PlaceholderText="ID";
            id.Location= new Point(100, 20);

            group=new ComboBox();
            group.Size= new Size(100, 20);
            group.Location= new Point(100, 40);

            cell =new TextBox();
            cell.Location = new Point(100, 60);
            cell.Size = new Size(100, 20);
            this.FormClosing += AddProductForm_FormClosing;

            Controls.AddRange(new Control[] {addImage,fullText,add,name,id,group,cell,cancelButton});
        }

        private void AddProductForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void Add_Click(object? sender, EventArgs e)
        {
            product.Name = name.Text;
            product.image = image;
            product.Group = group.SelectedItem.ToString();
            product.infoForProduct = fullTextS;
            product.cell = int.Parse(cell.Text.ToString());
        }

        private void AddProductForm_Activated(object? sender, EventArgs e)
        {
            for (int i = 0; i < Product.groupsP.Count; i++)
            {
                group.Items.Add(Product.groupsP[i]);
            }
        }

        private void AddImage_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK) 
            {
                image=Image.FromFile(openFileDialog.FileName);
            }
        }

        private void FullText_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(openFileDialog.FileName);
                fullTextS=streamReader.ReadToEnd();
            }
        }
    }
}
