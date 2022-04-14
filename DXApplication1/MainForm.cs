using DevExpress.XtraBars;
using DXApplication1.Constants;
using DXApplication1.ProductForms;
using DXApplication1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DXApplication1
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public static string Username = null;
        UserService userservice;
        ViewHide viewHide = ViewHide.View;
        public MainForm()
        {
            InitializeComponent();
            CheckAuthentication();
        }

        public void CheckAuthentication()
        {
            if(Username == null)
            {
                basesElement.Enabled = false;
                productsElement.Enabled = false;
                citiesElement.Enabled = false;
                logOutBtn.Enabled = false;
                logOutBtn.Visible = false;
            }
            else
            {
                basesElement.Enabled = true;
                productsElement.Enabled = true;
                citiesElement.Enabled = true;
                logOutBtn.Enabled = true;
                logOutBtn.Visible = true;
            }
        }

        Form OpenForm(Form inputForm)
        {
            CheckAuthentication();
            inputForm.TopLevel = false;
            inputForm.FormBorderStyle = FormBorderStyle.None;
            inputForm.Dock = DockStyle.Fill;
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(inputForm);
            inputForm.Show();
            return inputForm;
        }

        private void citiesElement_Click(object sender, EventArgs e)
        {
            var citiesForm = new CitiesForm();
            OpenForm(citiesForm);
        }

        private void basesElement_Click(object sender, EventArgs e)
        {
            OpenForm(new BasesForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var regForm = new RegistrationForm.RegistrationForm();
            OpenForm(regForm).FormClosed += (s, ev) => { OnFormCloseMethod(); };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(userNameTxtBox.Text) &&
                !string.IsNullOrEmpty(passwordTxtBox.Text))
            {
                Username = userNameTxtBox.Text;
                userservice = new UserService();
                var result = await userservice.LogIn(userNameTxtBox.Text, passwordTxtBox.Text);
                if (result)
                {
                    CheckAuthentication();
                    var welcome = new WelcomeForm();
                    OpenForm(welcome).FormClosed += (s, ev) => { OnFormCloseMethod(); };
                }
                else
                {
                    Username = null;
                }
            }
        }

        private async void logOutBtn_Click(object sender, EventArgs e)
        {
            userservice = new UserService();
            await userservice.LogOut();
            OnFormCloseMethod();
        }

        private void productsElement_Click(object sender, EventArgs e)
        {
            var prodForm = new ProductForm();
            OpenForm(prodForm).FormClosed += (s, ev) => { OnFormCloseMethod(); };
        }
        public void OnFormCloseMethod()
        {
            Controls.Clear();
            InitializeComponent();
            CheckAuthentication();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(viewHide == ViewHide.View)
            {
                passwordTxtBox.PasswordChar = new char();
                pictureBox1.Image = Properties.Resources.hide;
                viewHide = ViewHide.Hide;
            }
            else
            {
                passwordTxtBox.PasswordChar = '*';
                pictureBox1.Image = Properties.Resources.view;
                viewHide = ViewHide.View;
            }
        }
    }
}
