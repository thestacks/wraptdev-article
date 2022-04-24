namespace BooksManagement.Domain.Books.Features;

using BooksManagement.Domain.Books;
using SharedKernel.Dtos.BooksManagement.Book;
using SharedKernel.Exceptions;
using BooksManagement.Databases;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class DeleteBook
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public DeleteBookCommand(Guid book)
        {
            Id = book;
        }
    }

    public class Handler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly BooksDbContext _db;

        public Handler(BooksDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _db.Books
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (recordToDelete == null)
                throw new NotFoundException("Book", request.Id);

            _db.Books.Remove(recordToDelete);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}