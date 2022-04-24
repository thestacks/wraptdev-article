namespace BooksManagement.IntegrationTests.FeatureTests.Books;

using SharedKernel.Dtos.BooksManagement.Book;
using BooksManagement.SharedTestHelpers.Fakes.Book;
using SharedKernel.Exceptions;
using BooksManagement.Domain.Books.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class BookListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_book_list()
    {
        // Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var queryParameters = new BookParametersDto();

        await InsertAsync(fakeBookOne, fakeBookTwo);

        // Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books.Should().HaveCount(2);
    }
    
    [Test]
    public async Task can_get_book_list_with_expected_page_size_and_number()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var fakeBookThree = FakeBook.Generate(new FakeBookForCreationDto().Generate());
        var queryParameters = new BookParametersDto() { PageSize = 1, PageNumber = 2 };

        await InsertAsync(fakeBookOne, fakeBookTwo, fakeBookThree);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books.Should().HaveCount(1);
    }
    
    [Test]
    public async Task can_get_sorted_list_of_book_by_Title_in_asc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "bravo")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "alpha")
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "Title" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_book_by_Title_in_desc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "alpha")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "bravo")
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "-Title" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_book_by_PublicationYear_in_asc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 2)
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 1)
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "PublicationYear" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_book_by_PublicationYear_in_desc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 1)
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 2)
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "-PublicationYear" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_book_by_Description_in_asc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "bravo")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "alpha")
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "Description" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_get_sorted_list_of_book_by_Description_in_desc_order()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "alpha")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "bravo")
            .Generate());
        var queryParameters = new BookParametersDto() { SortOrder = "-Description" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
        books
            .Skip(1)
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookOne, options =>
                options.ExcludingMissingMembers());
    }

    
    [Test]
    public async Task can_filter_book_list_using_Title()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "alpha")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Title, _ => "bravo")
            .Generate());
        var queryParameters = new BookParametersDto() { Filters = $"Title == {fakeBookTwo.Title}" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books.Should().HaveCount(1);
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_book_list_using_PublicationYear()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 1)
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.PublicationYear, _ => 2)
            .Generate());
        var queryParameters = new BookParametersDto() { Filters = $"PublicationYear == {fakeBookTwo.PublicationYear}" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books.Should().HaveCount(1);
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
    }

    [Test]
    public async Task can_filter_book_list_using_Description()
    {
        //Arrange
        var fakeBookOne = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "alpha")
            .Generate());
        var fakeBookTwo = FakeBook.Generate(new FakeBookForCreationDto()
            .RuleFor(b => b.Description, _ => "bravo")
            .Generate());
        var queryParameters = new BookParametersDto() { Filters = $"Description == {fakeBookTwo.Description}" };

        await InsertAsync(fakeBookOne, fakeBookTwo);

        //Act
        var query = new GetBookList.BookListQuery(queryParameters);
        var books = await SendAsync(query);

        // Assert
        books.Should().HaveCount(1);
        books
            .FirstOrDefault()
            .Should().BeEquivalentTo(fakeBookTwo, options =>
                options.ExcludingMissingMembers());
    }

}