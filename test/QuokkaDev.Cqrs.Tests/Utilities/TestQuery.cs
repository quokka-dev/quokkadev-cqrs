namespace QuokkaDev.Cqrs.Tests.Utilities
{
    public class TestQuery
    {
        public string Message { get; set; } = "";
    }

    public class TestQueryResult
    {
        public const string DEFAULT_RESPONSE = "Mock Response";
        public string Message { get; set; } = "";
    }
}
