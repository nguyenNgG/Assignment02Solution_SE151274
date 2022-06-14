using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetList();
        public Task<Book> Get(int id);
        public Task Add(Book obj);
        public Task Update(Book obj);
        public Task Delete(int id);
    }
}
