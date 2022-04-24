namespace BooksManagement.Domain.Books.Features;

using SharedKernel.Dtos.BooksManagement.Book;
using SharedKernel.Exceptions;
using BooksManagement.Databases;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

public static class GetBook
{
    public class BookQuery : IRequest<BookDto>
    {
        public Guid Id { get; set; }

        public BookQuery(Guid id)
        {
            Id = id;
        }
    }

    public class Handler : IRequestHandler<BookQuery, BookDto>
    {
        private readonly BooksDbContext _db;
        private readonly IMapper _mapper;

        public Handler(BooksDbContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<BookDto> Handle(BookQuery request, CancellationToken cancellationToken)
        {
            var result = await _db.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

            if (result == null)
                throw new NotFoundException("Book", request.Id);

            return _mapper.Map<BookDto>(result);
        }
    }
}