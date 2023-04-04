using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallange.Service.Interface
{
    public interface IStooqService
    {
        Task<string> CallEndpointStooq(string message);
    }
}
