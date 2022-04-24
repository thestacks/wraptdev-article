namespace BooksManagement.Domain.Books.Mappings;

using SharedKernel.Dtos.BooksManagement.Book;
using AutoMapper;
using BooksManagement.Domain.Books;

public class BookProfile : Profile
{
    public BookProfile()
    {
        //createmap<to this, from this>
        CreateMap<Book, BookDto>()
            .ReverseMap();
        CreateMap<BookForCreationDto, Book>();
        CreateMap<BookForUpdateDto, Book>()
            .ReverseMap();
    }
}