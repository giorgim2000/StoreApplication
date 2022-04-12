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

namespace DXApplication1.CityForms
{
    public partial class CityDialog : Form
    {
        public static int cityId;
        CityService cityService;
        public CityDialog(string name)
        {
            InitializeComponent();
            Text = "Add City";
            button1.Text = "Add";
            label1.Text = "Add City";
        }
        public CityDialog(string name, string cityName, int editCityId)
        {
            InitializeComponent();
            Text = "Edit City";
            button1.Text = "Edit";
            label1.Text = "Edit City";
            textBox1.Text = cityName;
            cityId = editCityId;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (button1.Text == "Add")
                {
                    cityService = new CityService();
                    await cityService.AddCity(textBox1.Text);
                    this.Close();
                }
                else
                {
                    cityService = new CityService();
                    await cityService.ChangeCityName(cityId, textBox1.Text);
                    this.Close();
                }
            }
            
        }
    }
}
