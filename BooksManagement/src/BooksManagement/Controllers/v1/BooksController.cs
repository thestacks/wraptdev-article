namespace BooksManagement.Controllers.v1;

using BooksManagement.Domain.Books.Features;
using SharedKernel.Dtos.BooksManagement.Book;
using BooksManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/books")]
[ApiVersion("1.0")]
public class BooksController: ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Gets a single Book by ID.
    /// </summary>
    /// <response code="200">Book record returned successfully.</response>
    /// <response code="400">Book has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Book.</response>
    [ProducesResponseType(typeof(BookDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id:guid}", Name = "GetBook")]
    public async Task<ActionResult<BookDto>> GetBook(Guid id)
    {
        var query = new GetBook.BookQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Books.
    /// </summary>
    /// <response code="200">Book list returned successfully.</response>
    /// <response code="400">Book has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Book.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<BookDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetBooks")]
    public async Task<IActionResult> GetBooks([FromQuery] BookParametersDto bookParametersDto)
    {
        var query = new GetBookList.BookListQuery(bookParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Creates a new Book record.
    /// </summary>
    /// <response code="201">Book created.</response>
    /// <response code="400">Book has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Book.</response>
    [ProducesResponseType(typeof(BookDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddBook")]
    public async Task<ActionResult<BookDto>> AddBook([FromBody]BookForCreationDto bookForCreation)
    {
        var command = new AddBook.AddBookCommand(bookForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetBook",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Updates an entire existing Book.
    /// </summary>
    /// <response code="204">Book updated.</response>
    /// <response code="400">Book has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Book.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id:guid}", Name = "UpdateBook")]
    public async Task<IActionResult> UpdateBook(Guid id, BookForUpdateDto book)
    {
        var command = new UpdateBook.UpdateBookCommand(id, book);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Book record.
    /// </summary>
    /// <response code="204">Book deleted.</response>
    /// <response code="400">Book has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Book.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id:guid}", Name = "DeleteBook")]
    public async Task<ActionResult> DeleteBook(Guid id)
    {
        var command = new DeleteBook.DeleteBookCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
