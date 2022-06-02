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
        public Task<Book> Get(int id) => BookDAO.Instance.Get(id);
        public Task<List<Book>> GetList() => BookDAO.Instance.GetList();
        public Task Add(Book obj) => BookDAO.Instance.Add(obj);
        public Task Update(Book obj) => BookDAO.Instance.Update(obj);
        public Task Delete(int id) => BookDAO.Instance.Delete(id);
    }
}
