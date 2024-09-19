//Directiva para trabajar con tokens y su validacion en JSON Web Token (JWT)
using Microsoft.IdentityModel.Tokens;

//Directiva para manejar la creacion y manipulacion de tokens JWT
using System.IdentityModel.Tokens.Jwt;

//Directiva para definir y trabajar con reclamaciones de identidad del usuario
using System.Security.Claims;

//Directiva para trabajar con codificacion de txto t bytes
using System.Text;

namespace HYCM20240905.Properties.Auth
{
    public class JwtAuthenticationService: IJwtAuthenticationService
    {
       
            private readonly string _key;

            public JwtAuthenticationService(string key)
            {
                _key = key;
            }

            public string Authenticate(string username)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.ASCII.GetBytes(_key);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, username)
                    }),
                    Expires = DateTime.UtcNow.AddHours(8),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
}
