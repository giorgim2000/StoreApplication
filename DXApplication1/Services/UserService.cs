using DXApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.Services
{
    public class UserService
    {
        public UserService()
        {

        }

        public async Task<bool> Register(UserForRegistrationDto user)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.BaseAddress = new Uri($"https://localhost:7001/api/Authentication/RegisterUser");
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    HttpRequestMessage request = new HttpRequestMessage(
                            HttpMethod.Post, _httpClient.BaseAddress);
                    request.Content = JsonContent.Create(user);
                    var response = await _httpClient.SendAsync(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        MessageBox.Show("You have registered successfully!");
                        return true;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        MessageBox.Show($"{await response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
            return false;
        }
        public async Task<bool> LogIn(string userName, string password)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var req = JsonContent.Create<UserForLogInDto>(new UserForLogInDto()
                    {
                        Username = userName,
                        Password = password
                    });
                    var response = await _httpClient.PostAsync(
                        $"https://localhost:7001/api/Authentication/Authenticate", req);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MainForm.Username = userName;
                        return true;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        MessageBox.Show($"{await response.Content.ReadAsStringAsync()}");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
            return false;
        }
        public async Task LogOut()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    _httpClient.Timeout = new TimeSpan(0, 0, 30);
                    var response = await _httpClient.PostAsync("https://localhost:7001/api/Authentication/LogOut", null);
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        MainForm.Username =null;

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Couldn't Establish connection to the Database!");
            }
        }
    }
}
