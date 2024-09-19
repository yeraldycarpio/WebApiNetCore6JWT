namespace HYCM20240905.Properties.Endpoints
{
    public static class BodegaEndpoint
    {
        static List<object> data = new List<object>();

        public static void AddBodegaEndpoints(this WebApplication app)
        {
            app.MapGet("/bodega", () =>
            {
                return data;
            }).RequireAuthorization();

            app.MapPost("/bodega", (int id, string nombre, string descripcion, int precio, DateTime fecha) =>
            {
                var matricula = new
                {
                    id,  // Usando el id proporcionado
                    nombre,
                    descripcion,
                    precio,
                    fecha
                };
                data.Add(matricula);
                return data;
            }).RequireAuthorization();

            app.MapGet("/bodega/{id}", (int id) =>
            {
                var matricula = data.FirstOrDefault(m => m.GetType().GetProperty("id")?.GetValue(m).Equals(id) == true);
                if (matricula == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(matricula);
            }).RequireAuthorization();

            app.MapDelete("/bodega/{id}", (int id) =>
            {
                var matricula = data.FirstOrDefault(m => m.GetType().GetProperty("id")?.GetValue(m).Equals(id) == true);
                if (matricula == null)
                {
                    return Results.NotFound();
                }

                data.Remove(matricula);
                return Results.Ok(matricula);
            }).RequireAuthorization();
        }
    }
}
