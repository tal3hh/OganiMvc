using OganiApp.Core.Entities;
using OganiApp.Service.Utilities.Paginations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OganiApp.Service.Services.Interface
{
    public interface ICommentService
    {
        Task<Paginate<Comment>> AllComments(string search, int page, int take);
        Task<List<Comment>> ProductComment(int id);
        Task CreateAsync(Comment model);
        Task RemoveAsync(int id);
    }
}
