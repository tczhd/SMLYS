using System.Security.Principal;

namespace SMLYS.ApplicationCore.Interfaces.Base
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
