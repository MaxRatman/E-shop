using E_shop.Properties;
using System.CodeDom;
using System.ComponentModel;
using System.DirectoryServices;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace E_shop
{
    
    public partial class Form1 : Form
    {
        ToolStrip toolStrip;
        ToolStripButton home;//возращает на начальную страницу
        ToolStripLabel username;
        ToolStripButton shoppingCart;
        ToolStripSeparator stripSeparator;
        ToolStripButton deleteProductInCart;
        ToolStripTextBox idDeletedProduct;
        public TabControl tabControl1;
        //public BindingList<User> users;
        //BindingList<Admin> Admins;
        InputForm inputForm;
        public static TabPage productInfoTab;
        FlowLayoutPanel layoutPanel;
        AdminsForm adminsForm;
        Product product1;
        //List<Product> products;
        List<ProductPanel> productPanels;
        AddProductForm addProductForm1;
        ShopInfoLib shopInfo;
        public Form1()
        {
            InitializeComponent();
            shopInfo = new ShopInfoLib();
            //this.Activated += Form1_Activated;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Product.groupsP = new List<string>() { "Toy", "Clothes" };

            productInfoTab = new TabPage();

            layoutPanel= new FlowLayoutPanel();
            layoutPanel.Dock= DockStyle.Fill;
            inputForm = new InputForm();
            inputForm.ShowDialog();
            tabControl1 = new TabControl();
            tabControl1.TabPages.Add("ShopMKey", "ShopM");
            tabControl1.Dock = DockStyle.Fill;
            //tabControl1.TabPages.Add(startPage);
            tabControl1.TabPages[0].BackColor= Color.White;
            tabControl1.TabPages[0].AutoScroll= true;
            tabControl1.TabPages[0].Controls.Add(layoutPanel);
            Controls.Add(tabControl1);
            tabControl1.TabPages.Add(productInfoTab);

            ToolStrip strip = CreateMenuStrip();
            tabControl1.TabPages[0].Controls.Add(strip);
            UserInput();
        }



        void UserInput()
        {
            switch (inputForm.Choice)
            {
                case Result.yes:
                    bool ch = false;
                    for (int i = 0; i < shopInfo.users.Count; i++)
                    {
                        if (shopInfo.users[i].NameUser == inputForm.name.Text && shopInfo.users[i].Password == inputForm.password.Text)
                        {
                            username.Text = inputForm.name.Text;
                            this.Focus();
                            break;
                        }
                        else
                        {
                            for (int j = 0; j < shopInfo.Admins.Count; j++)
                            {
                                if (shopInfo.Admins[j].NameUser == inputForm.name.Text && shopInfo.Admins[j].Password == inputForm.password.Text)
                                {
                                    ch = true;
                                    this.Focus();
                                    username.Text = inputForm.name.Text;
                                    adminsForm = new AdminsForm(shopInfo.users);
                                    //adminsForm.NameAdmin = inputForm.name.Text;
                                    adminsForm.Show();
                                   // adminsForm.GotFocus += AdminsForm_GotFocus;
                                    //adminsForm.Activated += AdminsForm_Activated;
                                    break;
                                }
                            }
                            if (ch == true)
                            {
                                break;
                            }
                        }
                        
                        int u = 0;
                        while (users[i].NameUser != inputForm.name.Text && users[i].Password != inputForm.password.Text||
                            Admins[i].NameUser != inputForm.name.Text && Admins[i].Password != inputForm.password.Text)
                        {
                            u++;
                            if (u == 3)
                            {
                                this.Close();
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Password or user name is incorrect");
                                inputForm.ShowDialog();
                            }
                            if(users[i].NameUser == inputForm.name.Text && users[i].Password == inputForm.password.Text ||
                            Admins[i].NameUser == inputForm.name.Text && Admins[i].Password == inputForm.password.Text)
                            {
                                this.Focus();
                                username.Text = inputForm.name.Text;
                                adminsForm = new AdminsForm(users);
                                //adminsForm.NameAdmin = inputForm.name.Text;
                                adminsForm.ShowDialog();
                                adminsForm.GotFocus += AdminsForm_GotFocus;
                                //adminsForm.Activated += AdminsForm_Activated;
                                break;
                            }
                        }
                        this.Focus();
                        break;
                    
                    }
                    break;
                case Result.newUser:
                    users.Add(new User(inputForm.newName.Text, inputForm.newPassword.Text));
                    username.Text = inputForm.newName.Text;
                    break;  
                case Result.exit:
                    this.Close();
                    break;
            }
            
        }

        /*private void AdminsForm_Activated(object? sender, EventArgs e)
        {
            /*switch (adminsForm.ChoiceAdmin)
            {
                case ResultAdmin.nulll:
                    break;
                case ResultAdmin.userDelete:
                    break;
                case ResultAdmin.addProduct:
                    //addProductForm.groups = new List<string>();
                    addProductForm = new AddProductForm();
                    addProductForm.ShowDialog();
                    addProduct(addProductForm.product);
                    break;
                case ResultAdmin.deleteProduct:
                    break;
                case ResultAdmin.redactProduct:
                    break;
                case ResultAdmin.off:
                    this.Close();
                    break;
            }
        }*/

        void addProduct(Product product)
        {
            StreamReader streamReader = new StreamReader("ProductText.txt");
            string text = streamReader.ReadToEnd();
            ProductPanel productPanel = new ProductPanel(product);
            products.Add(product);
            productPanels.Add(productPanel);
            layoutPanel.Controls.Add(productPanel);
        }
        private void AdminsForm_GotFocus(object? sender, EventArgs e)
        {
            switch (adminsForm.ChoiceAdmin)
            {
                case ResultAdmin.off:
                    this.Close();
                    break;
                case ResultAdmin.userDelete:
                    break;
                case ResultAdmin.addProduct:
                    addProductForm1 = new AddProductForm();
                    addProduct(new Product(addProductForm1.image, addProductForm1.name.Text,
                        addProductForm1.group.SelectedValue.ToString(), int.Parse(addProductForm1.cell.Text), addProductForm1.fullTextS));
                    break;
                case ResultAdmin.deleteProduct: 
                    break;
                case ResultAdmin.redactProduct: 
                    break;
            }

        }

        ToolStrip CreateMenuStrip()
        {
            
            home = new ToolStripButton(null,Resources.house,Home_Click,"Home");

            username = new ToolStripLabel("User",Resources.user_generic2_black);

            shoppingCart = new ToolStripButton("ShoppingCart", 
                Resources.shopping_bag,ShoppingCart_Click);

            toolStrip = new ToolStrip(new ToolStripItem[] { home,new ToolStripSeparator(),
                shoppingCart,new ToolStripSeparator(),username });
            return toolStrip;
        }
        void CreateShoppinCartPages()
        {
            deleteProductInCart = new ToolStripButton("Delete",null,deleteProductInCart_Click);
            stripSeparator= new ToolStripSeparator();
            idDeletedProduct = new ToolStripTextBox("Enter id");

            ToolStrip strip=CreateMenuStrip();
            strip.Items.Add(stripSeparator);
            strip.Items.Add(deleteProductInCart);
            strip.Items.Add(idDeletedProduct);
            TabPage cart = new TabPage();
            cart.Text=shoppingCart.Text;
            cart.Controls.Add(strip);
            tabControl1.TabPages.Add(cart);
        }
        private void deleteProductInCart_Click(object? sender, EventArgs e)
        {

        }
        private void ShoppingCart_Click(object? sender, EventArgs e)
        {
            CreateShoppinCartPages();
            tabControl1.SelectedIndex=tabControl1.TabPages.Count-1;
        }

        private void Home_Click(object? sender, EventArgs e)
        {
            tabControl1.SelectedIndex = tabControl1.TabPages.IndexOfKey("ShopMKey");
        }

    }
    class ProductPanel:Panel
    {
        Image? image { get; set; }
        string name { get; set; }
        int id;
        string group { get; set; }
        int cell { get; set; }
        string infoForProduct { get; set; }
        Product product;
        Color backColor;
        Point location;
        Label nameLabel;
        Label cellLabel;
        Label groupLabel;
        PictureBox pictureBox;
        Button infoButton;
        public ProductPanel(Product product)
        {
            backColor = Color.GreenYellow;
            this.product = product;
            this.image = product.image;
            this.name = product.Name;
            //this.id = id;
            this.group = product.Group;
            this.cell = product.cell;
            this.infoForProduct= product.infoForProduct;
            this.Size = new Size(150, 150);

            cellLabel = new Label();
            cellLabel.Text = cell.ToString();
            cellLabel.BorderStyle= BorderStyle.Fixed3D;
            cellLabel.Location = new Point(95, 115);
            cellLabel.Size = new Size(50,30);
            cellLabel.TextAlign = ContentAlignment.MiddleCenter;
            cellLabel.BackColor = backColor;

            groupLabel = new Label();
            groupLabel.Text = group;
            groupLabel.BorderStyle= BorderStyle.Fixed3D;
            groupLabel.Size= new Size(40,20);
            groupLabel.BackColor=backColor;
            groupLabel.Location = new Point(105, 95);

            pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.Location = new Point(35, 0);
            pictureBox.BackColor = backColor;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox.Size = new Size(70, 70);

            nameLabel = new Label();
            nameLabel.Text = name;
            nameLabel.BorderStyle= BorderStyle.Fixed3D;
            nameLabel.BackColor = backColor;
            nameLabel.Size = new Size(70,20);
            nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            nameLabel.Location = new Point(35,70);

            infoButton= new Button();
            infoButton.Text = "More info";
            infoButton.Size = new Size(95, 30);
            infoButton.Location = new Point(-1,115);
            infoButton.BackColor = backColor;
            infoButton.Click += InfoButton_Click;

            this.BorderStyle = BorderStyle.Fixed3D;
            this.Dock = DockStyle.None;
            this.BackColor= Color.DarkSlateBlue;
            this.Controls.Add(nameLabel);
            this.Controls.Add(cellLabel);
            this.Controls.Add(groupLabel);
            this.Controls.Add(pictureBox);
            this.Controls.Add(infoButton);
        }
        private void InfoButton_Click(object? sender, EventArgs e)
        {
             Form1.productInfoTab.Controls.Clear();
             ProductInfo productInfo=new ProductInfo(new Product(image, name,group,cell,infoForProduct));
             Form1.productInfoTab.Controls.Add(productInfo);
        }
    }
    public class User
    {
        public  string NameUser { get; set; }
        int id { get; set; }
        public string Password { get; set; }
        public User(string name, string password)
        {
            NameUser = name;
            this.Password = password;
        }
    }
    public class Admin:User
    {
        public string name { get; set; }
        int id { get; set; }
        public string password { get; set; }
        public Admin(string name, string password):base (name,password)
        {
            this.name = name;
            this.password = password;
        }
    }
    class ProductInfo:Panel
    {
        Product product1;
        Image? image { get; set; }
        string name { get; set; }
        int id;
        string group { get; set; }
        int cell { get; set; }
        public ProductInfo(Product product)
        {
            product1= product;
            this.image = product1.image;
            this.name = product1.Name;
            this.group = product1.Group;
            this.cell = product1.cell;
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.DarkSlateBlue;

            PictureBox pictureBox= new PictureBox();
            pictureBox.Image= image;
            pictureBox.Location= new Point(0,0);
            pictureBox.Size = new Size(128,128);
            pictureBox.BorderStyle = BorderStyle.Fixed3D;

            Label NameLabel = new Label();
            NameLabel.Text= name;
            NameLabel.Dock= DockStyle.Top;

            Label GroupLabel= new Label();
            GroupLabel.Dock=DockStyle.Bottom;

            Label CellLabel= new Label();
            CellLabel.Text = cell.ToString();
            CellLabel.Dock= DockStyle.Right;
            this.Controls.AddRange(new Control[]{ pictureBox,NameLabel,GroupLabel,CellLabel });
            //tabPage.Controls.Add(this);
        }
    }
    public class Product
    {
        public static List<string> groupsP { get; set; }
        public Image? image { get; set; }
        public string Name { get; set; }
        public int id { get; set; }
        public string Group { get; set; }
        public int cell { get; set; }
        public string infoForProduct { get; set; }
        public Product(Image image,string name, string group, int cell, string infoForProduct)
        {
            this.image = image;
            Name = name;
            this.id = id;
            Group = group;
            this.cell = cell;
            this.infoForProduct = infoForProduct;
        }
    }
    public class ShopInfoLib
    {
        public BindingList<User> users;
        public BindingList<Admin> Admins;
        public List<Product> products;
        public ShopInfoLib()
        {
            users.Add(new User("Bob", "123"));
            users.Add(new User("Tom", "321"));
            Admins.Add(new Admin("Admin", "777"));
            Admins.Add(new Admin("Admin1", "7771"));
        }

    }


}