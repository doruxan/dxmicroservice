namespace OPLOGMicroservice.Infra.Exceptions
{
    public enum ErrorCode
    {
        ResourceAlreadyExists = 0,
        ResourceNotFound = 1,
        InvalidContentType = 2,
        InvalidCredential = 3,
        InvalidOperation = 4,
        UnsupportedType = 5,
        ResourceAlreadyFulfilled = 6,
        MaestroPackageCancelSyncFailed = 7,
        WmsPackageCancelSyncFailed = 8
    }

    // This class is generated for swagger code generator
    public class OplogError
    {
        public ErrorCode ErrorCode { get; set; }
    }

    public static class ErrorMessages
    {
        public const string Unhandled = "Unhandled exception caught!";
    }
}
