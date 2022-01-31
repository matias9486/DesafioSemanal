using DesafioSemanal.Model;
using DesafioSemanal.ViewModel.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DesafioSemanal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly SignInManager<UserIdentity> _signInManager;
        public AuthenticationController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Registro")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            //revisar si existe usuario
            var userExists = await _userManager.FindByNameAsync(model.Username);

            //si existe usuario devolver error
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            //sino existe registrar usuario
            var user = new UserIdentity
            {
                UserName = model.Username,
                Email=model.Email,
                IsActive=true

             };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Status = "Error",
                        Message = $"User creation failed! Errors:{string.Join(",", result.Errors.Select(x=>x.Description))}"
                    });
            }

            return Ok(new
                {
                Status = "Success",
                Message = $"User created successfully!"
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //chequear que el usuario exista y la password sea correcta
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            { 
                var currentUser= await _userManager.FindByNameAsync(model.Username);
                if (currentUser.IsActive)
                {
                    //generar token, devolver token creado.                    
                    return Ok( await GetToken(currentUser));
                }
            }

            return StatusCode(StatusCodes.Status401Unauthorized,
                    new
                    {
                        Status = "Error",
                        Message = $"El usuario {model.Username} no está autorizado! "
                    });
        }

        private async Task< LoginResponseViewModel> GetToken(UserIdentity currentUser)
        {
            //levantamos los roles que tiene el usuario
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currentUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeAutorizacion"));

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return new LoginResponseViewModel 
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };

        }

    }
}
