using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<Book>> GetBooks(string query)
        {
            var db = new eBookStoreDbContext();
            List<Book> books = null;
            if (string.IsNullOrWhiteSpace(query))
            {
                books = await db.Books.Include(x => x.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).ToListAsync();
            }
            else
            {
                query = query.ToLower();
                books = await db.Books.Include(x => x.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).Where(x => x.Title.ToLower().Contains(query) || x.Price.ToString().Contains(query)).ToListAsync();
            }
            return books;
        }

        public async Task<Book> GetBook(int id)
        {
            var db = new eBookStoreDbContext();
            Book book = await db.Books.Include(x => x.Publisher).Include(x => x.BookAuthors).ThenInclude(x => x.Author).FirstOrDefaultAsync(x => x.BookId == id);
            return book;
        }

        public async Task AddBook(Book book)
        {
            var db = new eBookStoreDbContext();
            db.Books.Add(book);
            await db.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            var db = new eBookStoreDbContext();
            db.Books.Update(book);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var db = new eBookStoreDbContext();
            Book book = new Book { BookId = id };
            db.Books.Attach(book);
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }
    }
}
