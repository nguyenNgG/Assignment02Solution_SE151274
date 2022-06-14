using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetList();
        public Task<User> Get(int id);
        public Task Add(User obj);
        public Task Update(User obj);
        public Task Delete(int id);
        public Task<User> Login(string email, string password);
    }
}
