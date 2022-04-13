using DXApplication1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.RegistrationForm
{
    public partial class RegistrationForm : Form
    {
        UserService userService;
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void registerBtn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(usernameTxtBox.Text)&&
                !string.IsNullOrEmpty(passwordTxtBox.Text)&&
                !string.IsNullOrEmpty(emailTxtBox.Text)&&
                !string.IsNullOrEmpty(phoneTxtBox.Text))
            {
                userService = new UserService();
                var result = await userService.Register(new Models.UserForRegistrationDto()
                {
                    Username = usernameTxtBox.Text,
                    Password = passwordTxtBox.Text,
                    Email = emailTxtBox.Text,
                    Phone = phoneTxtBox.Text
                });

                if (result)
                    this.Close();
            }
        }
    }
}
