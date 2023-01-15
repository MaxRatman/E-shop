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
    public enum Result{yes, no, exit,newUser};
    public partial class InputForm : Form
    {

        public Result Choice;
        Label text;
        Button inputWithoutRegistration;
        Button inputWithRegistration;
        Button exit;
        Panel panel1;
        public TextBox name;
        public TextBox password;
        Label ex;
        Panel panel2;
        Button input;
        Button registerPanel;
        Panel panel3;
        public TextBox newName;
        public TextBox newPassword;
        Button register;

        private void InputForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(450, 200);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;

            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            Controls.Add(panel1);

            exit = new Button();
            exit.Text = "Exit";
            exit.Dock = DockStyle.Fill;
            exit.Click += Exit_Click;
            panel1.Controls.Add(exit);

            inputWithoutRegistration = new Button();
            inputWithoutRegistration.Text = "Input \nWithout \nRegistration";
            inputWithoutRegistration.Dock = DockStyle.Left;
            inputWithoutRegistration.Click += InputWithoutRegistration_Click;
            panel1.Controls.Add(inputWithoutRegistration);

            inputWithRegistration = new Button();
            inputWithRegistration.Text = "Input \nWith \nRegistration";
            inputWithRegistration.Dock = DockStyle.Right;
            inputWithRegistration.Click += InputWithRegistration_Click;
            panel1.Controls.Add(inputWithRegistration);

            text = new Label();
            text.Dock = DockStyle.Top;
            text.Text = "Before you go to our online store you must choose the type of entrance.";
            panel1.Controls.Add(text);

            this.FormClosed += InputForm_FormClosed;
        }

        private void InputForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Choice = Result.exit;
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            Form1.ActiveForm.Close();
        }

        private void InputWithoutRegistration_Click(object? sender, EventArgs e)
        {
            Choice = Result.no;
            this.Hide();
        }

        private void InputWithRegistration_Click(object? sender, EventArgs e)
        {
            Choice = Result.yes;

            panel1.Visible = false;

            name = new TextBox();
            name.Location=new Point(0,0);

            password = new TextBox();
            password.Location = new Point(name.Size.Width+1, 0);

            ex = new Label();
            ex.Text = "Name | password";
            ex.Location=new Point(name.Size.Width*2+1, 0);
            ex.BorderStyle = BorderStyle.Fixed3D;

            input = new Button();
            input.Dock = DockStyle.Bottom;
            input.Text = "Input";
            input.Click += Input_Click;

            registerPanel= new Button();
            registerPanel.Size=new Size(100,100);
            registerPanel.Location=new Point(0,20);
            registerPanel.Text = "Register";
            registerPanel.Click += Register_Click;

            panel2 = new Panel();
            panel2.Dock = DockStyle.Fill;
            Controls.Add(panel2);
            panel2.Controls.Add(name);
            panel2.Controls.Add(password);
            panel2.Controls.Add(ex);
            panel2.Controls.Add(input);
            panel2.Controls.Add(registerPanel);

            this.Size = new Size(name.Size.Width * 3 + 20, this.Size.Height);
        }

        private void Register_Click(object? sender, EventArgs e)
        {
            panel2.Visible= false;

            newName = new TextBox();
            newName.Location = new Point(0, 0);

            newPassword = new TextBox();
            newPassword.Location =new Point(name.Width + 1,0);

            register = new Button();
            register.Dock= DockStyle.Bottom;
            register.Click += Register_Click1;

            panel3 = new Panel();
            panel3.Dock= DockStyle.Fill;
            panel3.Controls.AddRange(new Control[] { newName,newPassword,register });
            Controls.Add(panel3);
        }

        private void Register_Click1(object? sender, EventArgs e)
        {
            Choice = Result.newUser;
            this.Hide();
        }

        private void Input_Click(object? sender, EventArgs e) 
        { 
            Choice = Result.yes;
            this.Hide();
        }

        public InputForm()
        {
            InitializeComponent();
        }
    }
}
