using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alura.ListaLeitura.Seguranca;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Alura.WebAPI.AuthProvider
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;

        public LoginController(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateToken([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(loginModel.Login, loginModel.Password, true, true);

            if (!result.Succeeded) return Unauthorized();

            //claims
            var direitos = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, loginModel.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("alura-webapi-authentication-valid"));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "Alura.WebApp",
                    audience: "Postman",
                    claims: direitos,
                    signingCredentials: credenciais,
                    expires: DateTime.Now.AddMinutes(30)
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token).ToString());
        }
    }
}