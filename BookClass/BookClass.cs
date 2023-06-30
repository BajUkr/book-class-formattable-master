using System;
using System.Globalization;

public sealed class Book : IEquatable<Book>, IComparable<Book>, IComparable, IFormattable
{
    private bool published;
    private DateTime datePublished;
    private int totalPages;

    public Book(string author, string title, string publisher) : this(author, title, publisher, string.Empty)
    {
    }

    public Book(string author, string title, string publisher, string isbn)
    {
        Author = author;
        Title = title;
        Publisher = publisher;
        ISBN = isbn;
    }

    public string Author { get; }
    public string Title { get; }
    public string Publisher { get; }
    public string ISBN { get; }
    public decimal Price { get; private set; }
    public string ?Currency { get; private set; }

    public int Pages
    {
        get => totalPages;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            totalPages = value;
        }
    }

    public void Publish(DateTime date)
    {
        published = true;
        datePublished = date;
    }

    public string GetPublicationDate()
    {
        return published ? datePublished.ToString("d", CultureInfo.InvariantCulture) : "Not published yet.";
    }

    public void SetPrice(decimal price, string currency)
    {
        Price = price;
        Currency = currency;
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
    {
        return Equals(obj as Book);
    }

    public bool Equals(Book other) => other != null && ISBN == other.ISBN;

    public override int GetHashCode()
    {
        return ISBN.GetHashCode();
    }

    public int CompareTo(Book other)
    {
        return other == null ? 1 : Title.CompareTo(other.Title);
    }

    int IComparable.CompareTo(object obj)
    {
        return CompareTo(obj as Book);
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        if (string.IsNullOrEmpty(format)) format = "G";
        if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

        switch (format.ToUpperInvariant())
        {
            case "G":
                return $"{Title} by {Author}";
            case "D":
                return $"{Title} by {Author}. {datePublished.Year}. {Publisher}. ISBN: {ISBN}. {Pages} pages. {Currency}{Price:F2}.";
            case "P":
                return $"{Title} by {Author}. {datePublished.Year}. {Publisher}. ISBN: {ISBN}. {Pages} pages.";
            case "Y":
                return $"{Title} by {Author}. {datePublished.Year}. {Publisher}. {Pages} pages.";
            case "T":
                return $"{Title} by {Author}. {datePublished.Year}. {Pages} pages.";
            case "R":
                return $"{Title} by {Author} {Currency}{Price:F2}.";
            default:
                throw new FormatException($"The {format} format string is not supported.");
        }
    }
}
