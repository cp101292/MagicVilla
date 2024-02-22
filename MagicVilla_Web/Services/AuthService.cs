using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly string _authUrl;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _authUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDto loginRequestDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = _authUrl + "/api/UsersAuth/login/"
            }
            );
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDto registrationRequestDto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = _authUrl + "/api/UsersAuth/register/"
            }
            );
        }
    }
}
