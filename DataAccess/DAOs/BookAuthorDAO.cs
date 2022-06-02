using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAOs
{
    internal class BookAuthorDAO
    {
        private static BookAuthorDAO instance = null;
        private static readonly object instanceLock = new object();
        private BookAuthorDAO() { }

        public static BookAuthorDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BookAuthorDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<BookAuthor> Get(int bookId, int authorId)
        {
            var db = new eBookStoreDbContext();
            BookAuthor obj = await db.BookAuthors.Include(x => x.Book).Include(x => x.Author).FirstOrDefaultAsync(x => x.AuthorId == authorId && x.BookId == bookId);
            return obj;
        }

        public async Task<List<BookAuthor>> GetList()
        {
            var db = new eBookStoreDbContext();
            List<BookAuthor> list = null;
            list = await db.BookAuthors.Include(x => x.Author).Include(x => x.Book).ToListAsync();
            return list;
        }

        public async Task Add(BookAuthor obj)
        {
            var db = new eBookStoreDbContext();
            db.BookAuthors.Add(obj);
            await db.SaveChangesAsync();
        }

        public async Task Update(BookAuthor obj)
        {
            var db = new eBookStoreDbContext();
            db.BookAuthors.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int bookId, int authorId)
        {
            var db = new eBookStoreDbContext();
            BookAuthor obj = new BookAuthor { BookId = bookId, AuthorId = authorId};
            db.BookAuthors.Attach(obj);
            db.BookAuthors.Remove(obj);
            await db.SaveChangesAsync();
        }
    }
}
