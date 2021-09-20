using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Alura.ListaLeitura.Seguranca
{
    public class SigningConfigurations
    {
        private readonly string secret = "mysupersecret_secretkey!123";

        public SigningConfigurations()
        {
            var keyByteArray = Encoding.ASCII.GetBytes(secret);
            Key = new SymmetricSecurityKey(keyByteArray);
            SigningCredentials = new SigningCredentials(
                Key,
                SecurityAlgorithms.HmacSha256
            );
        }

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}