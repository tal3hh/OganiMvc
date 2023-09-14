using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IContactService
    {
        Task CreateAsync(Contact entity);
        Task<List<Contact>> GetAllAsync();
        Task RemoveAsync(int id);
    }
}
