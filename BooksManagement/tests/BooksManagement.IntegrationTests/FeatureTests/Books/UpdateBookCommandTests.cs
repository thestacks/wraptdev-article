namespace BooksManagement.IntegrationTests.FeatureTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.IntegrationTests.TestUtilities;
using SharedKernel.Dtos.BooksManagement.Book;
using SharedKernel.Exceptions;
using BooksManagement.Domain.Books.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using static TestFixture;

public class UpdateBookCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_book_in_db()
    {
        // Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var updatedBookDto = new FakeBookForUpdateDto().Generate();
        await InsertAsync(fakeBookOne);

        var book = await ExecuteDbContextAsync(db => db.Books.SingleOrDefaultAsync());
        var id = book.Id;

        // Act
        var command = new UpdateBook.UpdateBookCommand(id, updatedBookDto);
        await SendAsync(command);
        var updatedBook = await ExecuteDbContextAsync(db => db.Books.Where(b => b.Id == id).SingleOrDefaultAsync());

        // Assert
        updatedBook.Should().BeEquivalentTo(updatedBookDto, options =>
            options.ExcludingMissingMembers());
    }
}