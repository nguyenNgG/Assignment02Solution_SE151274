using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
