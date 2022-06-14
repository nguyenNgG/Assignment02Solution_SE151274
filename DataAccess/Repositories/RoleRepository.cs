using BusinessObject;
using DataAccess.DAOs;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task Add(Role obj)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Role> Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Role> GetList() => RoleDAO.Instance.GetList();

        public Task Update(Role obj)
        {
            throw new NotImplementedException();
        }
    }
}
