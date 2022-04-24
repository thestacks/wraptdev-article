namespace BooksManagement.UnitTests.UnitTests.Domain.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.Domain.Books;
using Bogus;
using FluentAssertions;
using NUnit.Framework;

public class UpdateBookTests
{
    private readonly Faker _faker;

    public UpdateBookTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_book()
    {
        // Arrange
        var fakeBook = FakeBook.Generate();
        var updatedBook = new FakeBookForUpdateDto().Generate();
        
        // Act
        fakeBook.Update(updatedBook);

        // Assert
        fakeBook.Should().BeEquivalentTo(updatedBook, options =>
            options.ExcludingMissingMembers());
    }
}