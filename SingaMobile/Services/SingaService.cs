using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SingaMobile.Models;

namespace SingaMobile.Services
{
    internal class SingaService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<string> Register(string email, string password)
        {
            var json = JsonConvert.SerializeObject(new RegistrationModel(email, password));
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync("http://176.112.213.5/api/account/register", content);

            if (response.IsSuccessStatusCode)
                return "Created";

            return JsonConvert.DeserializeObject<List<RegistrationErrorModel>>(await response.Content.ReadAsStringAsync())
                    .FirstOrDefault().description;
        }
    }
}