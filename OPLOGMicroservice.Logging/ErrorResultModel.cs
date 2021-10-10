namespace OPLOGMicroservice.Logging
{
    public class ErrorResultModel
    {
        public string ErrorId { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string CorrelationId { get; set; }
    }
}
