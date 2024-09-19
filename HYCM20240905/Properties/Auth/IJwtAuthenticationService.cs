namespace HYCM20240905.Properties.Auth
{
    //Interfaz para un servicio de autenticacion JWT
    public interface IJwtAuthenticationService
    {
        //Metodo para autenticar al usuario y genera un token JWT
        string Authenticate(string name);
    }
}
