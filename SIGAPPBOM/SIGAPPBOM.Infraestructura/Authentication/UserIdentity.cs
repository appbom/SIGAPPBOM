using System.Security.Principal;

namespace SIGAPPBOM.Infraestructura.Authentication
{
    public interface IUserIdentity : IIdentity
    {
        int Id { get; }
    }

    public class UserIdentity : IUserIdentity
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public UserIdentity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}