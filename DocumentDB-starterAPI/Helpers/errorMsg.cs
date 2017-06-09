namespace DocumentDB_starterAPI.Helpers
{
    /// <summary>
    /// List of errors
    /// </summary>
    public class ErrorMsg
    {
        public const string BDD01 = "Database error";
        public const string BDD02 = "Collection error";
        public const string BDD03 = "GetOneDocumentAsync error";
        public const string BDD04 = "InsertDocument error";
        public const string BDD05 = "UpdateDocument error";
        public const string BDD06 = "DeleteDocument error";
        public const string BDD07 = "GetDocumentsAsync error";
        public const string BDD08 = "GetAllDocumentsAsync error";
        public const string BDD09 = "GetAllSortedDocumentsAsync error";
    }
}