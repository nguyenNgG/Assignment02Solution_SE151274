using BusinessObject;
using DataAccess.DAOs;
using DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<List<User>> GetList() => UserDAO.Instance.GetList();
        public Task<User> Get(int id) => UserDAO.Instance.Get(id);
        public Task Add(User obj) => UserDAO.Instance.Add(obj);
        public Task Update(User obj) => UserDAO.Instance.Update(obj);
        public Task Delete(int id) => UserDAO.Instance.Delete(id);
        public Task<User> Login(string email, string password) => UserDAO.Instance.Login(email, password);
    }
}
