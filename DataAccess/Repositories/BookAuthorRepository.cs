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
    public class BookAuthorRepository : IBookAuthorRepository
    {
        public Task<List<BookAuthor>> GetList() => BookAuthorDAO.Instance.GetList();
        public Task<BookAuthor> Get(int bookId, int authorId) => BookAuthorDAO.Instance.Get(bookId, authorId);
        public Task Add(BookAuthor obj) => BookAuthorDAO.Instance.Add(obj);
        public Task Update(BookAuthor obj) => BookAuthorDAO.Instance.Update(obj);
        public Task Delete(int bookId, int authorId) => BookAuthorDAO.Instance.Delete(bookId, authorId);
    }
}
