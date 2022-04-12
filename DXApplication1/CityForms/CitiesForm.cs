using DXApplication1.CityForms;
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

namespace DXApplication1
{
    public partial class CitiesForm : Form
    {
        CityService cityService;
        public CitiesForm()
        {
            InitializeComponent();
            LoadCities();
        }
        public async void LoadCities()
        {
            dataGridView1.Rows.Clear();
            cityService = new CityService();
            await cityService.GetData();
            if(CityService.Cities != null)
            {
                foreach (var city in CityService.Cities)
                {
                    dataGridView1.Rows.Add(city.Id, city.CityName);
                }
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            cityService = new CityService();
            if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                await cityService.DeleteCity((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }else if(dataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                var cityDialog = new CityDialog(
                    "edit",
                    dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                cityDialog.ShowDialog();
            }

            LoadCities();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var cityDialog = new CityDialog("add");
            cityDialog.ShowDialog();
            LoadCities();
        }
    }
}
