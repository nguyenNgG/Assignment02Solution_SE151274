using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetBooks(string query);
        public Task<Book> GetBook(int id);
        public Task AddBook(Book book);
        public Task UpdateBook(Book book);
        public Task DeleteBook(int id);
    }
}
