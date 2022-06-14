using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAOs
{
    internal class RoleDAO
    {
        private static RoleDAO instance = null;
        private static readonly object instanceLock = new object();
        private RoleDAO() { }

        public static RoleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RoleDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Role> GetList()
        {
            var db = new eBookStoreDbContext();
            List<Role> list = null;
            list = db.Roles.ToList();
            return list;
        }
    }
}
