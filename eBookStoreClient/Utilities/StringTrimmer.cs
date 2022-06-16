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

        public static Publisher TrimPublisher(Publisher publisher)
        {
            if (publisher != null)
            {
                publisher.PublisherName = publisher.PublisherName.Trim();
                publisher.City = publisher.City.Trim();
                publisher.State = publisher.State.Trim();
                publisher.Country = publisher.Country.Trim();
            }
            return publisher;
        }

        public static Book TrimBook(Book book)
        {
            if (book != null)
            {
                book.Title = book.Title.Trim();
                book.Notes = !string.IsNullOrWhiteSpace(book.Notes) ? book.Notes.Trim() : "";
            }
            return book;
        }

        public static User TrimUser(User user)
        {
            if (user != null)
            {
                user.EmailAddress = user.EmailAddress.Trim();
                user.FirstName = user.FirstName.Trim();
                user.LastName = user.LastName.Trim();
                user.MiddleName = user.MiddleName.Trim();
                user.Source = user.Source.Trim();
            }
            return user;
        }
    }
}
