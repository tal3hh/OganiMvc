using OganiApp.Core.Entities;
using OganiApp.Data.UniteOfWork;
using OganiApp.Service.Services.Interface;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services
{
    public class ContactService : IContactService
    {
        private readonly IUow _uow;
        public ContactService( IUow uow)
        {
            _uow = uow;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            var list = await _uow.GetRepository<Contact>().AllAsync();

            return list;
        }

        public async Task CreateAsync(Contact entity)
        {
            if (entity != null)
            {
                await _uow.GetRepository<Contact>().CreateAsync(entity);

                await _uow.SaveChangesAsync();
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);

            if (entity != null)
            {
                _uow.GetRepository<Contact>().Remove(entity);
                await _uow.SaveChangesAsync();
            }
        }
    }
}
