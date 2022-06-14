using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class BookDAO
    {
        private static BookDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookDAO() { }

        public static BookDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Book>> GetList()
        {
            var db = new eBookStoreDbContext();
            List<Book> books = null;
            books = await db.Books.Include(x => x.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).ToListAsync();
            return books;
        }

        public async Task<Book> Get(int id)
        {
            var db = new eBookStoreDbContext();
            Book book = await db.Books.Include(x => x.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).FirstOrDefaultAsync(x => x.BookId == id);
            return book;
        }

        public async Task Add(Book book)
        {
            var db = new eBookStoreDbContext();
            db.Books.Add(book);
            await db.SaveChangesAsync();
        }

        public async Task Update(Book book)
        {
            var db = new eBookStoreDbContext();
            db.Books.Update(book);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var db = new eBookStoreDbContext();
            Book book = new Book { BookId = id };
            db.Books.Attach(book);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }
    }
}
