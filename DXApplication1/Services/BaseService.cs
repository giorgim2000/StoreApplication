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
    public class BaseService
    {
        public static List<BaseDto> Bases;
        public BaseService()
        {
            Bases = null;
        }
        public async Task GetData()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri("https://localhost:7001/api/bases?pageSize=20&pageNumber=1");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await _httpClient.GetAsync("");
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var rawData = await response.Content.ReadAsStringAsync();
                        var data = Serilizer<List<BaseDto>>.JsonSerilizeData(rawData);
                        Bases = data;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
        public async Task AddBase(int cityId, BaseForCreationDto newBase)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"https://localhost:7001/api/bases?cityId={cityId}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Post, _httpClient.BaseAddress);
                    request.Content = JsonContent.Create<BaseForCreationDto>(newBase);
                    var response = await _httpClient.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("Bases has been added!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        MessageBox.Show("City Id is incorrect!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
                throw;
            }
        }

        public async Task UpdateBase(int cityId, BaseForUpdateDto baseDto)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"https://localhost:7001/api/bases?cityId={cityId}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    HttpRequestMessage request = new HttpRequestMessage(
                        HttpMethod.Put, _httpClient.BaseAddress);
                    request.Content = JsonContent.Create(baseDto);
                    var response = await _httpClient.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        MessageBox.Show("Base has been Edited!");
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        MessageBox.Show("City Id is incorrect!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
                throw;
            }
        }

        public async Task DeteleBase(int id)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"https://localhost:7001/api/bases?baseId={id}");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.DeleteAsync("");
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Couldn't find the Base!");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
                throw;
            }
        }
    }
}
