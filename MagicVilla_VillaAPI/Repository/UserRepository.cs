using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MagicVilla_VillaAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        private readonly string _secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _db = db;
            _secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public bool IsUniqueUser(string username)   
        {
            //var user = _db.LocalUsers.SingleOrDefault(x => x.Name.Equals(username));
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Name.Equals(username));
            return user == null;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            //var user = _db.LocalUsers.SingleOrDefault(x => x.UserName == loginRequestDto.UserName &&
            //                                               x.Password == loginRequestDto.Password);
            var user = _db.ApplicationUsers.SingleOrDefault(x => x.UserName.ToLower().Equals(loginRequestDto.UserName.ToLower()));
            if (user == null)
            {
                return new LoginResponseDto() { Token = "", User = null };
            }

            if (!_userManager.CheckPasswordAsync(user, loginRequestDto.Password).GetAwaiter().GetResult())
            {
                return new LoginResponseDto() { Token = "", User = null };
            }


            
            //Generate the JWT token if user is valid.
            //Getting the roles assigned to the user.
            var roles = await _userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Null check on UserName and Roles can be handled : TODO
                    new(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                // To serialize the token. 
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user),
                //Role = roles.FirstOrDefault() // We will not pass role here as we will extract it from Token.
            };
            return loginResponseDto;
        }

        public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.UserName,
                Email = registrationRequestDto.UserName,
                NormalizedUserName = registrationRequestDto.UserName.ToUpper(),
                Name = registrationRequestDto.Name,
            };

            try
            {
                var result  = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    await _userManager.AddToRoleAsync(user, "admin");
                    var userToReturn =
                        _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDto.UserName);
                    if (userToReturn != null)
                    {
                       return  _mapper.Map<UserDto>(user);
                    }
                }
            }
            catch (Exception e)
            {
                return new UserDto();
            }
            return new UserDto();
        }
    }
}
