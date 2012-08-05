namespace SIGAPPBOM.Logistica.Infraestructura.Authentication
{
    public interface IAuthenticationService
    {
        bool ValidaUsuario(string usuario, string password);
        IUserPrincipal ObtienerInformacionUsuario();
    }
}
