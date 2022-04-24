namespace BooksManagement.UnitTests.UnitTests.Domain.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.Domain.Books;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

public class CreateBookTests
{
    private readonly Faker _faker;

    public CreateBookTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_book()
    {
        // Arrange + Act
        var fakeBook = Book.Create(new FakeBookForCreationDto().Generate());

        // Assert
        fakeBook.Should().NotBeNull();
    }
}