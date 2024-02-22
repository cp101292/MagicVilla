using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly string _villaNumberUrl;

        public VillaNumberService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _villaNumberUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data=dto,
                Url = _villaNumberUrl + "/api/v1/VillaNumberAPI",
                Token = token
            }
            );
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = _villaNumberUrl + "/api/v1/VillaNumberAPI/" + id,
                    Token = token
                }
            );
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaNumberUrl + "/api/v1/VillaNumberAPI",
                    Token = token
            }
            );
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaNumberUrl + "/api/v1/VillaNumberAPI/" + id,
                    Token = token

                }
            );
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.PUT,
                    Data = dto,
                    Url = _villaNumberUrl + "/api/v1/VillaNumberAPI/" + dto.VillaNo,
                    Token = token
                }
            );
        }
    }
}
