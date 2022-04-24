namespace SharedKernel.Dtos.BooksManagement.Book
{
    using SharedKernel.Dtos.Shared;

    public class BookParametersDto : BasePaginationParameters
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}