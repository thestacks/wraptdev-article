namespace BooksManagement.Domain.Books;

using SharedKernel.Dtos.BooksManagement.Book;
using BooksManagement.Domain.Books.Mappings;
using BooksManagement.Domain.Books.Validators;
using AutoMapper;
using FluentValidation;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Book : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Title { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int PublicationYear { get; private set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Description { get; private set; }


    public static Book Create(BookForCreationDto bookForCreationDto)
    {
        new BookForCreationDtoValidator().ValidateAndThrow(bookForCreationDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<BookProfile>();
        }));
        var newBook = mapper.Map<Book>(bookForCreationDto);
        
        return newBook;
    }
        
    public void Update(BookForUpdateDto bookForUpdateDto)
    {
        new BookForUpdateDtoValidator().ValidateAndThrow(bookForUpdateDto);
        var mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.AddProfile<BookProfile>();
        }));
        mapper.Map(bookForUpdateDto, this);
    }
    
    private Book() { } // For EF
}