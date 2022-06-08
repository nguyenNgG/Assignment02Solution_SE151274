using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO() { }

        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<User>> GetList()
        {
            var db = new eBookStoreDbContext();
            List<User> list = null;
            list = await db.Users.Include(x => x.Role).Include(x => x.Publisher).ToListAsync();
            return list;
        }

        public async Task<User> Get(int id)
        {
            var db = new eBookStoreDbContext();
            User obj = await db.Users.Include(x => x.Role).Include(x => x.Publisher).FirstOrDefaultAsync(x => x.UserId == id);
            return obj;
        }

        public async Task Add(User obj)
        {
            var db = new eBookStoreDbContext();
            db.Users.Add(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(User obj)
        {
            var db = new eBookStoreDbContext();
            db.Users.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var db = new eBookStoreDbContext();
            User obj = new User { UserId = id };
            db.Users.Attach(obj);
            db.Users.Remove(obj);
            await db.SaveChangesAsync();
        }
        public async Task<User> Login(string email, string password)
        {
            var db = new eBookStoreDbContext();
            User obj = await db.Users.FirstOrDefaultAsync(x => x.EmailAddress == email && x.Password == password);
            return obj;
        }
    }
}
