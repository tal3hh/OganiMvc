using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Models.Account
{
    public class RoleAssignSendModel
    {
        public List<RoleAssingListModel>? Roles { get; set; }
        public int UserId { get; set; }
    }
}
