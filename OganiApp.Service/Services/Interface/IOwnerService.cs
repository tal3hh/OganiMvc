using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IOwnerService
    {
        List<Owner> GetOwners();
        Task<List<Owner>> GetAllAsync();
        Task CreateAsync(Owner model);
        Task<Owner> GetById(int id);
        Task<Owner> GetByIdUpdate(int id);
        Task Update(Owner model);
        Task Remove(int id);

    }
}
