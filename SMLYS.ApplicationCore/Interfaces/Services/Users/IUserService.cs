using SMLYS.ApplicationCore.DTOs.User;
namespace SMLYS.ApplicationCore.Interfaces.Services.Users
{
    public interface IUserService
    {
        UserContext GetUserContextAsync(string userId);
    }
}
