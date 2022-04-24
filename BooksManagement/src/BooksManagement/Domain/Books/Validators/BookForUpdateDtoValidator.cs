namespace BooksManagement.Domain.Books.Validators;

using SharedKernel.Dtos.BooksManagement.Book;
using FluentValidation;

public class BookForUpdateDtoValidator: BookForManipulationDtoValidator<BookForUpdateDto>
{
    public BookForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}