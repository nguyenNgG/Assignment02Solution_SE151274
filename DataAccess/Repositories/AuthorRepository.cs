using BusinessObject;
using DataAccess.DAOs;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<List<Author>> GetList() => AuthorDAO.Instance.GetList();
        public Task<Author> Get(int id) => AuthorDAO.Instance.Get(id);
        public Task Add(Author obj) => AuthorDAO.Instance.Add(obj);
        public Task Update(Author obj) => AuthorDAO.Instance.Update(obj);
        public Task Delete(int id) => AuthorDAO.Instance.Delete(id);
    }
}
