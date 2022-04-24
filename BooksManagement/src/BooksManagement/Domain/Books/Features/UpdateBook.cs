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

public static class UpdateBook
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public BookForUpdateDto BookToUpdate { get; set; }

        public UpdateBookCommand(Guid book, BookForUpdateDto newBookData)
        {
            Id = book;
            BookToUpdate = newBookData;
        }
    }

    public class Handler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly BooksDbContext _db;
        private readonly IMapper _mapper;

        public Handler(BooksDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = await _db.Books
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (bookToUpdate == null)
                throw new NotFoundException("Book", request.Id);

            bookToUpdate.Update(request.BookToUpdate);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}