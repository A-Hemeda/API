namespace Demo.API.HandleResponse
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode , string message = null)
        {
            StatusCode = statusCode ;
            Message = message ?? GetStatusCodeMessage(statusCode);
        }

        private string GetStatusCodeMessage (int code)
        {
            return code switch
            {
                400 => "Bad Request",
                401 => "You are not autherized!!",
                404 => "Resource not found",
                500 => "Internal Server error",
                _ => null
            };
        }
    }
}
