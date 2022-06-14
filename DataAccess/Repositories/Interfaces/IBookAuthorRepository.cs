using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookAuthorRepository
    {
        public Task<List<BookAuthor>> GetList();
        public Task<BookAuthor> Get(int bookId, int authorId);
        public Task Add(BookAuthor obj);
        public Task Update(BookAuthor obj);
        public Task Delete(int bookId, int authorId);
    }
}
