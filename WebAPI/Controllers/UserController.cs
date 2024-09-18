using AutoMapper;
using Contracts;
using Entities.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly IOptions<AppSettings> options;
        private readonly IMapper mapper;
        private readonly JwtSecurityTokenHandler securityTokenHandler;
        
        public UserController(IRepositoryManager repositoryManager, IOptions<AppSettings> options, IMapper mapper)
        {
            this.repositoryManager = repositoryManager ?? throw new ArgumentNullException(nameof(repositoryManager));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            securityTokenHandler = new JwtSecurityTokenHandler();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserAuthInfoDTO>> LoginAsync([FromBody] UserCredentials userCredentials, CancellationToken cancellationToken)
        {
            UserAuthInfoDTO userAuthInfoDTO = new UserAuthInfoDTO();

            if(userCredentials is null) 
            {
                return BadRequest("No data");
            }

            var user =  await repositoryManager.User.LoginAsync(userCredentials.Login, userCredentials.Password, false, cancellationToken);

            if (user != null) 
            {
                var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);

                var tokenDescriptoir = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity
                    (
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, user.FirstName),
                            new Claim(ClaimTypes.Name, user.LastName),
                            new Claim(ClaimTypes.Role, user.Role.ToString()),
                        }
                    ),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var securityToken = securityTokenHandler.CreateToken(tokenDescriptoir);
                userAuthInfoDTO.Token = securityTokenHandler.WriteToken(securityToken);
                userAuthInfoDTO.UserDetails = mapper.Map<UserDTO>(user);
            }

            if (string.IsNullOrEmpty(userAuthInfoDTO?.Token))
            {
                return Unauthorized("Error login or password");
            }

            return Ok(userAuthInfoDTO);
        }
    }
}