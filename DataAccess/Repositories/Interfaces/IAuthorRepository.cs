using BusinessObject;
using System.Collections.Generic;
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
