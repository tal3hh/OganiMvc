using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface IBlogDetailService
    {
        Task<List<Blog>> AllProDetaIsNull();
        Task CreateAsync(BlogDetail model);
        Task UpdateAsync(BlogDetail model);
        Task<BlogDetail> GetByUpdateIdAsync(int id);
        Task RemoveAsync(int id);
    }
}
