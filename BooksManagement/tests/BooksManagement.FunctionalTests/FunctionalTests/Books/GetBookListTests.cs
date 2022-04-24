namespace BooksManagement.FunctionalTests.FunctionalTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetBookListTests : TestBase
{
    [Test]
    public async Task get_book_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await _client.GetRequestAsync(ApiRoutes.Books.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}