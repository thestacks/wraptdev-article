namespace SharedKernel.Dtos.BooksManagement.Book
{
    using System.Collections.Generic;
    using System;

    public abstract class BookForManipulationDto 
    {
            public string Title { get; set; }
        public int PublicationYear { get; set; }
        public string Description { get; set; }
    }
}