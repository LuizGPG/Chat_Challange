using System.Threading.Tasks;

namespace ChatChallange.Service.Interface
{
    public interface IStooqService
    {
        Task<string> CallEndpointStooq(string message);
    }
}
