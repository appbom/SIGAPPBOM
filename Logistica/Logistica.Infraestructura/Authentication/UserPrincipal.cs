using System.Collections;
using System.Security.Principal;
using System.Collections.Generic;

namespace SIGAPPBOM.Logistica.Infraestructura.Authentication
{
    public interface IUserPrincipal : IPrincipal
    {
        IUserIdentity UserIdentity { get; set; }
        IList Roles { get; set; }
        IList<Menu> Menu { get; set; }
    }

    public class UserPrincipal : IUserPrincipal
    {
        public IList Roles { get; set; }

        public IList<Menu> Menu { get; set; }

        public IUserIdentity UserIdentity { get; set; }


        public UserPrincipal(IUserIdentity userIdentity, IList roles)
        {
            UserIdentity = userIdentity;
            Roles = roles;
        }

        public IIdentity Identity
        {
            get { return UserIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);

        }
    }
}
