using DXApplication1.Constants;
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
    public partial class ProductDialog : Form
    {
        ProductCreateDto createProduct;
        ProductUpdateDto updateProduct = new ProductUpdateDto();
        ProductService productService;
        DialogType dialogType;
        public ProductDialog()
        {
            InitializeComponent();
            dialogType = DialogType.Add;
        }
        public ProductDialog(ProductDto productDto)
        {
            InitializeComponent();
            label1.Text = "Edit Product";
            button1.Text = "Edit";
            updateProduct.Id = productDto.Id;
            productNameTxt.Text = productDto.ProductName;
            barcodeTxt.Text = productDto.BarCode;
            priceTxt.Text = productDto.Price.ToString();
            qtyTxt.Text = productDto.Quantity.ToString();
            dialogType = DialogType.Update;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            productService = new ProductService();

            if(priceTxt.Text.All(i => char.IsDigit(i)) &&
            qtyTxt.Text.All(c => char.IsDigit(c)))
            {
                if (dialogType == DialogType.Update)
                {
                    updateProduct.ProductName = productNameTxt.Text;
                    updateProduct.BarCode = barcodeTxt.Text;
                    updateProduct.Price = Convert.ToInt32(priceTxt.Text);
                    updateProduct.Quantity = Convert.ToInt32(qtyTxt.Text);
                    await productService.UpdateProduct(updateProduct);
                }
                else
                {
                    createProduct = new ProductCreateDto()
                    {
                        ProductName = productNameTxt.Text,
                        BarCode = barcodeTxt.Text,
                        Price = Convert.ToInt32(priceTxt.Text),
                        Quantity = Convert.ToInt32(qtyTxt.Text)
                    };
                    await productService.AddProduct(createProduct);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Price and Quantity inputs should contain only Digits!");
            }
            
        }
    }
}
