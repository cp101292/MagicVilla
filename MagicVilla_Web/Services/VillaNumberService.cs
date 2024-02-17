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
        public Task<T> CreateAsync<T>(VillaNumberCreateDto dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data=dto,
                Url = _villaNumberUrl + "/api/VillaNumberAPI"
            }
            );
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = _villaNumberUrl + "/api/VillaNumberAPI/" + id
                }
            );
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaNumberUrl + "/api/VillaNumberAPI"
            }
            );
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.GET,
                    Url = _villaNumberUrl + "/api/VillaNumberAPI/" + id
                }
            );
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDto dto)
        {
            return SendAsync<T>(new APIRequest()
                {
                    ApiType = SD.ApiType.PUT,
                    Data = dto,
                    Url = _villaNumberUrl + "/api/VillaNumberAPI/" + dto.VillaNo
                }
            );
        }
    }
}
