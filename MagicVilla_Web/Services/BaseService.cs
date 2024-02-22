using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        private readonly IHttpClientFactory httpClient;

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    SD.ApiType.POST => HttpMethod.Post,
                    SD.ApiType.PUT => HttpMethod.Put,
                    SD.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };

                HttpResponseMessage apiResp = null;
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResp = await client.SendAsync(message);

                var apiContent = await apiResp.Content.ReadAsStringAsync();

                try
                {
                    APIResponse apiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (apiResponse is { StatusCode: HttpStatusCode.BadRequest or HttpStatusCode.NotFound })
                    {
                        apiResponse.StatusCode = HttpStatusCode.BadRequest;
                        apiResponse.IsSuccess = false;

                        var res = JsonConvert.SerializeObject(apiResponse);
                        var returnResponse = JsonConvert.DeserializeObject<T>(res);
                        return returnResponse;
                    }
                }
                catch (Exception e)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception e)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
