using System.Runtime.CompilerServices;

namespace HYCM20240905.Properties.Endpoints
{
    // declara una clase estatica llamada "ProtectedEndpoint".
    public static class ProtectedEndpoint
    {
        static List<object> data = new List<object>();

        public static void AddcategoriaEndpoints(this WebApplication app)
        {
            app.MapGet("/categoria", () =>
            {
                return data;
            }).AllowAnonymous();

            app.MapPost("/categoria", (string nombre, string tipo) =>
            {
                data.Add(new { nombre, tipo });
                return data;
            }).RequireAuthorization();

        }
    }
}
