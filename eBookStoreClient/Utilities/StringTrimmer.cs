using BusinessObject;

namespace eStoreClient.Utilities
{
    public static class StringTrimmer
    {
        public static Author TrimAuthor(Author author)
        {
            if (author != null)
            {
                author.EmailAddress = author.EmailAddress.Trim();
                author.FirstName = author.FirstName.Trim();
                author.LastName = author.LastName.Trim();
                author.Zip = author.Zip.Trim();
                author.City = author.City.Trim();
                author.Phone = author.Phone.Trim();
            }
            return author;
        }

        public static Book TrimBook(Book book)
        {
            if (book != null)
            {
                book.Title = book.Title.Trim();
                book.Notes = book.Notes.Trim();
            }
            return book;
        }
    }
}
