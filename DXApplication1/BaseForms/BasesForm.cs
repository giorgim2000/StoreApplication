using DXApplication1.BaseForms;
using DXApplication1.Models;
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
    public partial class BasesForm : Form
    {
        BaseService baseService;
        public BasesForm()
        {
            InitializeComponent();
            LoadBases();
        }

        public async void LoadBases()
        {
            dataGridView1.Rows.Clear();
            baseService = new BaseService();
            await baseService.GetData();
            if(BaseService.Bases != null)
            {
                foreach (var bs in BaseService.Bases)
                {
                    dataGridView1.Rows.Add(bs.Id, bs.BaseName, bs.Adress, bs.CityName);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var bd = new BaseDialog();
            bd.ShowDialog();
            LoadBases();
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            baseService = new BaseService();
            if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                await baseService.DeteleBase((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                LoadBases();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                var baseDto = new BaseDto()
                {
                    Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value,
                    BaseName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Adress = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    CityName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()
                };
                var baseDialog = new BaseDialog(baseDto);
                baseDialog.ShowDialog();
                LoadBases();
            }
        }
    }
}
