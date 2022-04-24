namespace BooksManagement.Domain.Books.Validators;

using SharedKernel.Dtos.BooksManagement.Book;
using FluentValidation;

public class BookForCreationDtoValidator: BookForManipulationDtoValidator<BookForCreationDto>
{
    public BookForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}