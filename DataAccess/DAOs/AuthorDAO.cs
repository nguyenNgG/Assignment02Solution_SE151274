using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class AuthorDAO
    {
        private static AuthorDAO instance = null;
        private static readonly object instanceLock = new object();
        private AuthorDAO() { }

        public static AuthorDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AuthorDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Author>> GetList()
        {
            var db = new eBookStoreDbContext();
            List<Author> list = null;
            list = await db.Authors.Include(x => x.BookAuthors).ToListAsync();
            return list;
        }

        public async Task<Author> Get(int id)
        {
            var db = new eBookStoreDbContext();
            Author obj = await db.Authors.Include(x => x.BookAuthors).FirstOrDefaultAsync(x => x.AuthorId == id);
            return obj;
        }

        public async Task Add(Author obj)
        {
            var db = new eBookStoreDbContext();
            db.Authors.Add(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(Author obj)
        {
            var db = new eBookStoreDbContext();
            db.Authors.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var db = new eBookStoreDbContext();
            Author obj = new Author { AuthorId = id };
            db.Authors.Attach(obj);
            db.Authors.Remove(obj);
            await db.SaveChangesAsync();
        }
    }
}
