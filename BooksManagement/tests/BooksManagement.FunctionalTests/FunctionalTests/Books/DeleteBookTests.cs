namespace BooksManagement.FunctionalTests.FunctionalTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class DeleteBookTests : TestBase
{
    [Test]
    public async Task delete_book_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBook = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        await InsertAsync(fakeBook);

        // Act
        var route = ApiRoutes.Books.Delete.Replace(ApiRoutes.Books.Id, fakeBook.Id.ToString());
        var result = await _client.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}