using ArtGalleryAPI.Models.DTO;
using ArtGalleryAPI.Response;
using ArtGalleryWebApp.Response;
using Newtonsoft.Json;
using System;
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
            httpClient.BaseAddress = new Uri("http://localhost:5173");
        }
        public async Task<BaseResponse<UserDTO>> Login(AuthRequestDTO authRequest)
        {
            try
            {

                var response = await httpClient.PostAsJsonAsync("/Login", authRequest);

                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<UserDTO>>(await response.Content.ReadAsStringAsync());
                    return BaseResponse<UserDTO>.SuccessResult(apiResponse.User);
                }

                // Obrada grešaka
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    throw new HttpRequestException(errorResponse.Message);
                }

                throw new HttpRequestException("Nepoznata greška prilikom prijave.");
            }
            catch (Exception ex)
            {
                return BaseResponse<UserDTO>.FailureResult($"Greška prilikom prijave: {ex.Message}");
            }
        }


    }
}