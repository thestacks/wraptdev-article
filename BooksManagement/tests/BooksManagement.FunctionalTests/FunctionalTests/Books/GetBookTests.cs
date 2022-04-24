namespace BooksManagement.FunctionalTests.FunctionalTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBookTests : TestBase
{
    [Test]
    public async Task get_book_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeBook = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        await InsertAsync(fakeBook);

        // Act
        var route = ApiRoutes.Books.GetRecord.Replace(ApiRoutes.Books.Id, fakeBook.Id.ToString());
        var result = await _client.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}