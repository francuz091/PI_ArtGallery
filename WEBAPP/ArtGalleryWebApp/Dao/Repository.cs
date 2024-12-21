using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;

namespace ArtGalleryWebApp.Dao
{
    public class Repository
    {
        private readonly HttpClient httpClient;

        public Repository()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5051");
        }
        public async Task<string> Login(string username, string password)
        {
            try
            {
                var authRequest = new
                {
                    Username = username,
                    Password = password
                };

                var response = await httpClient.PostAsJsonAsync("Login", authRequest);

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    return responseString; // Vraća korisnika u JSON formatu
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return "Neispravna lozinka.";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return "Korisnik nije pronađen.";
                }
                else
                {
                    return "Greška prilikom prijave.";
                }
            }
            catch (Exception ex)
            {
                return $"Greška prilikom prijave: {ex.Message}";
            }
        }
    }
}