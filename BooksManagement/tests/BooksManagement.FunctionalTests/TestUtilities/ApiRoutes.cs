namespace BooksManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class Books
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/books";
        public const string GetRecord = $"{Base}/books/{Id}";
        public const string Create = $"{Base}/books";
        public const string Delete = $"{Base}/books/{Id}";
        public const string Put = $"{Base}/books/{Id}";
        public const string Patch = $"{Base}/books/{Id}";
        public const string CreateBatch = $"{Base}/books/batch";
    }
}
