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
            if (!string.IsNullOrEmpty(isbn))
            {
                if (!(isbn.Length == 10 | isbn.Length == 13))
                    throw new ArgumentException("The ISBN must be a 10 or 13 character numeric string.");
                if (!UInt64.TryParse(isbn, out _))
                    throw new ArgumentException("The ISBN can consist of numeric characters only");
            }
            ISBN = isbn;

            Author = author;
        }

        public string ISBN { get; }

        public string Author { get; }

        public decimal Price { get; private set; }

        public string Currency { get; private set; }

        public decimal SetPrice(decimal price, string currency)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException("The price cannot be negative.");
            decimal oldValue = Price;
            Price = price;

            if (currency.Length != 3)
                throw new ArgumentOutOfRangeException("The ISO currency string is a 3 character string.");
            Currency = currency;

            return oldValue;
        }

        public override bool Equals(object obj)
        {
            return obj switch
            {
                Book b => b.ISBN == ISBN,
                _ => false,
            };
        }

        public override int GetHashCode() => ISBN.GetHashCode();

        public override string ToString() => $"{(string.IsNullOrEmpty(Author) ? "" : Author + ", ")}{Title}";
    }
}
