using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public List<Role> GetList();
        public Task<Role> Get(int id);
        public Task Add(Role obj);
        public Task Update(Role obj);
        public Task Delete(int id);
    }
}
