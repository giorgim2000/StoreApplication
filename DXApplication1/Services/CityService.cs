using DXApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.Services
{
    public class CityService
    {
        public static List<CityDto> Cities;
        public CityService()
        {
            Cities = null;
        }
        
        public async Task GetData()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/cities");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await _httpClient.GetAsync("");
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var rawData = await response.Content.ReadAsStringAsync();
                        var data = Serilizer<List<CityDto>>.JsonSerilizeData(rawData);
                        Cities = data;
                    }
                }
            }catch(Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task AddCity(string cityName)
        {
            try
            {
                using(var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/cities?cityName={cityName}");
                    _httpClient.Timeout=new TimeSpan(0, 0, 30);
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Post, _httpClient.BaseAddress);
                    var response = await _httpClient.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("City has been added!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        MessageBox.Show("City name was invalid!");
                }
            }catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task ChangeCityName(int cityId, string cityName)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/cities?cityId={cityId}&cityName={cityName}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Put, _httpClient.BaseAddress);
                    var response = await _httpClient.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("City has been Edited!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        MessageBox.Show("City name was invalid!");
                    else
                        MessageBox.Show("Couldn't find the City!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task DeleteCity(int id)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"{MainForm.ApiAdress}/cities/{id}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.DeleteAsync("");
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("City cannot be deleted!");
                    }
                    else
                        MessageBox.Show("The city has been Deleted!");
                }
            }catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
    }
}
