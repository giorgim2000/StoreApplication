using DXApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DXApplication1.Services
{
    public class UserService
    {
        public UserService()
        {

        }

        public async Task Register(UserForRegistrationDto user)
        {
            using (var _httpClient = new HttpClient())
            {

            }
        }
        public async Task LogIn(UserForLogInDto user)
        {
            using (var _httpClient = new HttpClient())
            {

            }
        }
        public async Task LogOut(UserForLogInDto user)
        {
            using (var _httpClient = new HttpClient())
            {

            }
        }
    }
}
