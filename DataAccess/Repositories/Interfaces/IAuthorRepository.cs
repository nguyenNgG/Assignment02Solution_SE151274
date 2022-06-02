using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public Task<List<Author>> GetList();
        public Task<Author> Get(int id);
        public Task Add(Author obj);
        public Task Update(Author obj);
        public Task Delete(int id);
    }
}
