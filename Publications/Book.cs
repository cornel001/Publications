using System;

namespace Publications
{
    public sealed class Book : Publication
    {
        public Book(string title, string author, string publisher) :
            this(title, string.Empty, author, publisher)
        { }

        public Book(string title, string isbn, string author, string publisher) :
            base(title, publisher, PublicationType.Book)
        {
            if (! string.IsNullOrEmpty(isbn))
            {
                if (!(isbn.Length == 10 | isbn.Length == 13))
                    throw new ArgumentException("The ISBN must be a 10 or 13 character numeric string.");
                ulong nISBN = 0;
                if (! UInt64.TryParse(isbn, out nISBN))
                    throw new ArgumentException("The ISBN can consist of numeric characters only");
            }
            ISBN = isbn;

            Author = author;
        }

        public string ISBN { get; }

        public string Author { get; }
    }
}
