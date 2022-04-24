namespace BooksManagement.IntegrationTests.FeatureTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.IntegrationTests.TestUtilities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using BooksManagement.Domain.Books.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddBookCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_book_to_db()
    {
        // Arrange
        var fakeBookOne = new FakeBookForCreationDto().Generate();

        // Act
        var command = new AddBook.AddBookCommand(fakeBookOne);
        var bookReturned = await SendAsync(command);
        var bookCreated = await ExecuteDbContextAsync(db => db.Books.SingleOrDefaultAsync());

        // Assert
        bookReturned.Should().BeEquivalentTo(fakeBookOne, options =>
            options.ExcludingMissingMembers());
        bookCreated.Should().BeEquivalentTo(fakeBookOne, options =>
            options.ExcludingMissingMembers());
    }
}