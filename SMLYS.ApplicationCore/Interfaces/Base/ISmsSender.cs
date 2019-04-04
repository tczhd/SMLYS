
using System.Threading.Tasks;

namespace SMLYS.ApplicationCore.Interfaces.Base
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
