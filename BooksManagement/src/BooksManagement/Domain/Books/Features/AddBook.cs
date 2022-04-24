namespace BooksManagement.Domain.Books.Features;

using BooksManagement.Domain.Books;
using SharedKernel.Dtos.BooksManagement.Book;
using SharedKernel.Exceptions;
using BooksManagement.Databases;
using BooksManagement.Domain.Books.Validators;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class AddBook
{
    public class AddBookCommand : IRequest<BookDto>
    {
        public BookForCreationDto BookToAdd { get; set; }

        public AddBookCommand(BookForCreationDto bookToAdd)
        {
            BookToAdd = bookToAdd;
        }
    }

    public class Handler : IRequestHandler<AddBookCommand, BookDto>
    {
        private readonly BooksDbContext _db;
        private readonly IMapper _mapper;

        public Handler(BooksDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<BookDto> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = Book.Create(request.BookToAdd);
            _db.Books.Add(book);

            await _db.SaveChangesAsync(cancellationToken);

            var bookAdded = await _db.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == book.Id, cancellationToken);

            return _mapper.Map<BookDto>(bookAdded);
        }
    }
}