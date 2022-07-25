using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace falcon2.Core.Services
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string receiverEmail, string receiverFirstName, string Link);
    }
}
