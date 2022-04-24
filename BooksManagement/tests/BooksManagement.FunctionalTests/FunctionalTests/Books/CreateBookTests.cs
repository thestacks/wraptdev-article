namespace BooksManagement.FunctionalTests.FunctionalTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateBookTests : TestBase
{
    [Test]
    public async Task create_book_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeBook = new FakeBookForCreationDto { }.Generate();

        // Act
        var route = ApiRoutes.Books.Create;
        var result = await _client.PostJsonRequestAsync(route, fakeBook);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}