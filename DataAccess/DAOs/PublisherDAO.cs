using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class PublisherDAO
    {
        private static PublisherDAO instance = null;
        private static readonly object instanceLock = new object();
        private PublisherDAO() { }

        public static PublisherDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PublisherDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Publisher>> GetList()
        {
            var db = new eBookStoreDbContext();
            List<Publisher> list = null;
            list = await db.Publishers.Include(x => x.Books).ToListAsync();
            return list;
        }

        public async Task<Publisher> Get(int id)
        {
            var db = new eBookStoreDbContext();
            Publisher obj = await db.Publishers.Include(x => x.Books).FirstOrDefaultAsync(x => x.PublisherId == id);
            return obj;
        }

        public async Task Add(Publisher obj)
        {
            var db = new eBookStoreDbContext();
            db.Publishers.Add(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(Publisher obj)
        {
            var db = new eBookStoreDbContext();
            db.Publishers.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var db = new eBookStoreDbContext();
            Publisher obj = new Publisher { PublisherId = id };
            db.Publishers.Attach(obj);
            db.Publishers.Remove(obj);
            await db.SaveChangesAsync();
        }
    }
}
