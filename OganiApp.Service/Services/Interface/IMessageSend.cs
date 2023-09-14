using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IMessageSend
    {
        void MimeKitConfrim(AppUser appUser, string url, string token);
        void MimeMessageResetPassword(AppUser user, string url, string code);
    }
}
