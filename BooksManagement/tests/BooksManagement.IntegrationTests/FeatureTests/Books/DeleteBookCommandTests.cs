namespace BooksManagement.IntegrationTests.FeatureTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.IntegrationTests.TestUtilities;
using BooksManagement.Domain.Books.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteBookCommandTests : TestBase
{
    [Test]
    public async Task can_delete_book_from_db()
    {
        // Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        await InsertAsync(fakeBookOne);
        var book = await ExecuteDbContextAsync(db => db.Books.SingleOrDefaultAsync());
        var id = book.Id;

        // Act
        var command = new DeleteBook.DeleteBookCommand(id);
        await SendAsync(command);
        var bookResponse = await ExecuteDbContextAsync(db => db.Books.ToListAsync());

        // Assert
        bookResponse.Count.Should().Be(0);
    }

    [Test]
    public async Task delete_book_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteBook.DeleteBookCommand(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_book_from_db()
    {
        // Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        await InsertAsync(fakeBookOne);
        var book = await ExecuteDbContextAsync(db => db.Books.SingleOrDefaultAsync());
        var id = book.Id;

        // Act
        var command = new DeleteBook.DeleteBookCommand(id);
        await SendAsync(command);
        var deletedBook = (await ExecuteDbContextAsync(db => db.Books
            .IgnoreQueryFilters()
            .ToListAsync())
        ).FirstOrDefault();

        // Assert
        deletedBook?.IsDeleted.Should().BeTrue();
    }
}