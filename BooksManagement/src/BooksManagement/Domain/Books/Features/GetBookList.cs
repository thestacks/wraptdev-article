namespace BooksManagement.Domain.Books.Features;

using BooksManagement.Domain.Books;
using SharedKernel.Dtos.BooksManagement.Book;
using SharedKernel.Exceptions;
using BooksManagement.Databases;
using BooksManagement.Wrappers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Sieve.Models;
using Sieve.Services;
using System.Threading;
using System.Threading.Tasks;

public static class GetBookList
{
    public class BookListQuery : IRequest<PagedList<BookDto>>
    {
        public BookParametersDto QueryParameters { get; set; }

        public BookListQuery(BookParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public class Handler : IRequestHandler<BookListQuery, PagedList<BookDto>>
    {
        private readonly BooksDbContext _db;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(BooksDbContext db, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<BookDto>> Handle(BookListQuery request, CancellationToken cancellationToken)
        {
            var collection = _db.Books
                as IQueryable<Book>;

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "Id",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider);

            return await PagedList<BookDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}