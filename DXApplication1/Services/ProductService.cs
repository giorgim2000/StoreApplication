using DXApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.Services
{
    public class ProductService
    {
        public static List<ProductDto> Products;
        public async Task GetData()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/products");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await _httpClient.GetAsync("");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var rawData = await response.Content.ReadAsStringAsync();
                        var data = Serilizer<List<ProductDto>>.JsonSerilizeData(rawData);
                        Products = data;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task AddProduct(ProductCreateDto product)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/products");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.PostAsync("", JsonContent.Create(product));
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("Product has been added!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        MessageBox.Show("The Inputs were invalid!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task UpdateProduct(ProductUpdateDto product)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/products/UpdateProduct");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.PutAsync("", JsonContent.Create(product));
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        MessageBox.Show("Product has been Edited!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        MessageBox.Show("Inputs were invalid!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task DeleteProduct(int id)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/products?productId={id}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.DeleteAsync("");
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Product cannot be deleted!");
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("The Product has been Deleted!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
    }
}
