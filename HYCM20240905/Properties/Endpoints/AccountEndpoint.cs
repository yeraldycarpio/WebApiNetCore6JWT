// Importa el espacio de nombres "AutenticacionJWTMinimalAPpi.Auth"
//para poder usar sus clases y tipos 
using HYCM20240905.Properties.Auth;
using HYCM20240905AutenticacionJWTMinimalApi.Auth;

namespace HYCM20240905AutenticacionJWTMinimalApi.Auth
{
    
    public static class AccountEndpoint
    {
        public static void AddAccountEndpoint(this WebApplication app)
        {
            app.MapPost("/account/login", (string login, string password, IJwtAuthenticationService authService) =>
            {
                if (login == "admin" && password == "admin")
                {
                    var token = authService.Authenticate(login);

                    return Results.Ok(token);
                }
                else
                {
                    return Results.Unauthorized();
                }
            });
        }
    }
}
