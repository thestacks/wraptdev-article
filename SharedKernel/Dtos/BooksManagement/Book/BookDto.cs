namespace SharedKernel.Dtos.BooksManagement.Book
{
    using System.Collections.Generic;
    using System;

    public class BookDto 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Description { get; set; }
    }
}