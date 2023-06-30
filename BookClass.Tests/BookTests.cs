using System;
using System.Globalization;
using NUnit.Framework;

namespace Test;
[TestFixture]
public class BookTests
{
    private Book? book;

    [SetUp]
    public void SetUp()
    {
        book = new Book("Jon Skeet", "C# in Depth", "Manning Publications", "9781617294532");
        book.Pages = 528;
        book.SetPrice(39.99m, "USD");
    }

    [Test]
    public void TestConstructorProperties()
    {
        Assert.AreEqual("Jon Skeet", book.Author);
        Assert.AreEqual("C# in Depth", book.Title);
        Assert.AreEqual("Manning Publications", book.Publisher);
        Assert.AreEqual("9781617294532", book.ISBN);
    }

    [Test]
    public void TestPagesProperty()
    {
        Assert.AreEqual(528, book.Pages);
    }

    [Test]
    public void TestPriceAndCurrencyProperties()
    {
        Assert.AreEqual(39.99m, book.Price);
        Assert.AreEqual("USD", book.Currency);
    }

    [Test]
    public void TestPublishMethod()
    {
        DateTime publicationDate = new DateTime(2019, 1, 1);
        book.Publish(publicationDate);
        Assert.AreEqual("01/01/2019", book.GetPublicationDate());
    }

    [Test]
    public void TestToString()
    {
        Assert.AreEqual("C# in Depth by Jon Skeet", book.ToString());
    }

    [Test]
    public void TestEqualsAndHashCode()
    {
        Book otherBook = new Book("Jon Skeet", "C# in Depth", "Manning Publications", "9781617294532");
        Assert.IsTrue(book.Equals(otherBook));
        Assert.AreEqual(book.GetHashCode(), otherBook.GetHashCode());
    }

    [Test]
    public void TestCompareTo()
    {
        Book otherBook = new Book("Jon Skeet", "C# in Action", "Manning Publications", "9781617294521");
        Assert.AreEqual(1, book.CompareTo(otherBook));
    }

    [Test]
    public void TestIFormattableToString()
    {
        DateTime publicationDate = new DateTime(2019, 1, 1);
        book.Publish(publicationDate);
        string format = "D";
        string formattedOutput = book.ToString(format, CultureInfo.InvariantCulture);
        Assert.AreEqual("C# in Depth by Jon Skeet. 2019. Manning Publications. ISBN: 9781617294532. 528 pages. USD39.99.", formattedOutput);
    }
}
