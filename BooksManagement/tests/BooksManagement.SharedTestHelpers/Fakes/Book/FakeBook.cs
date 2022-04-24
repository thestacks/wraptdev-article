namespace BooksManagement.SharedTestHelpers.Fakes.Book;

using AutoBogus;
using BooksManagement.Domain.Books;
using SharedKernel.Dtos.BooksManagement.Book;

public class FakeBook
{
    public static Book Generate(BookForCreationDto bookForCreationDto)
    {
        return Book.Create(bookForCreationDto);
    }

    public static Book Generate()
    {
        return Book.Create(new FakeBookForCreationDto().Generate());
    }
}