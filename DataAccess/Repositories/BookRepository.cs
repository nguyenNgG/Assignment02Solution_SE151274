using BusinessObject;
using DataAccess.DAOs;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task<Book> GetBook(int id) => BookDAO.Instance.GetBook(id);
        public Task<List<Book>> GetBooks(string query) => BookDAO.Instance.GetBooks(query);
        public Task AddBook(Book book) => BookDAO.Instance.AddBook(book);
        public Task UpdateBook(Book book) => BookDAO.Instance.UpdateBook(book);
        public Task DeleteBook(int id) => BookDAO.Instance.DeleteBook(id);
    }
}
