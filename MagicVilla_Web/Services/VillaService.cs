using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.DataProtection;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private string _villaUrl;

        public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = _villaUrl + "/api/v1/VillaAPI",
                Token = token
            }
            );
        }   

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = _villaUrl + "/api/v1/VillaAPI/" + id,
                Token = token
            }
            );
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaUrl + "/api/v1/VillaAPI",
                    Token = token
                    
                }
            );
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaUrl + "/api/v1/VillaAPI/" + id,
                    Token = token
                }
            );
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = _villaUrl + "/api/v1/VillaAPI/" + dto.Id,
                Token = token
            }
            );
        }
    }
}
