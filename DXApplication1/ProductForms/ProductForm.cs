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

namespace DXApplication1.ProductForms
{
    public partial class ProductForm : Form
    {
        ProductService productService;
        public ProductForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        public async void LoadProducts()
        {
            dataGridView1.Rows.Clear();
            productService = new ProductService();
            await productService.GetData();
            foreach (var product in ProductService.Products)
            {
                dataGridView1.Rows.Add(
                    product.Id, product.ProductName,
                    product.BarCode, product.Price, product.Quantity
                    );
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            productService = new ProductService();
            if (dataGridView1.Columns[e.ColumnIndex].Name == "delete")
            {
                await productService.DeleteProduct((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                LoadProducts();
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "edit")
            {
                var productDto = new ProductDto()
                {
                    Id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value,
                    ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    BarCode = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Price =(int) dataGridView1.Rows[e.RowIndex].Cells[3].Value,
                    Quantity =(int) dataGridView1.Rows[e.RowIndex].Cells[4].Value
                };
                var productDialog = new ProductDialog(productDto);
                productDialog.ShowDialog();
                LoadProducts();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var productDialog = new ProductDialog();
            productDialog.ShowDialog();
            LoadProducts();
        }
    }
}
