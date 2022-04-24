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

public class BookQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_book_with_accurate_props()
    {
        // Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        await InsertAsync(fakeBookOne);

        // Act
        var query = new GetBook.BookQuery(fakeBookOne.Id);
        var books = await SendAsync(query);

        // Assert
        books.Should().BeEquivalentTo(fakeBookOne, options =>
            options.ExcludingMissingMembers());
    }

    [Test]
    public async Task get_book_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetBook.BookQuery(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}