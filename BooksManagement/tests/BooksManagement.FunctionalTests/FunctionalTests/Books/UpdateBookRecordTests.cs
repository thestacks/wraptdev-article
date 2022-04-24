namespace BooksManagement.FunctionalTests.FunctionalTests.Books;

using BooksManagement.SharedTestHelpers.Fakes.Book;
using BooksManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateBookRecordTests : TestBase
{
    [Test]
    public async Task put_book_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeBook = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var updatedBookDto = new FakeBookForUpdateDto { }.Generate();
        await InsertAsync(fakeBook);

        // Act
        var route = ApiRoutes.Books.Put.Replace(ApiRoutes.Books.Id, fakeBook.Id.ToString());
        var result = await _client.PutJsonRequestAsync(route, updatedBookDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}