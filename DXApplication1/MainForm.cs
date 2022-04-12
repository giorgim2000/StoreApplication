using DevExpress.XtraBars;
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
        public static int UserId = 0;
        public MainForm()
        {
            InitializeComponent();
            if (Username == null && UserId == 0)
            {
                basesElement.Enabled = false;
                productsElement.Enabled = false;
                citiesElement.Enabled = false;
            }
        }

        Form OpenForm(Form inputForm)
        {
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
            OpenForm(new CitiesForm());
        }

        private void basesElement_Click(object sender, EventArgs e)
        {
            OpenForm(new BasesForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var regForm = new RegistrationForm.RegistrationForm();
            OpenForm(regForm).FormClosed += (s, ev) =>
            {
                Controls.Clear();
                InitializeComponent();
            };
        }
    }
}
