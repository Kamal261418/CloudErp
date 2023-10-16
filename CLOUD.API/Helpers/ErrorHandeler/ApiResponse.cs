namespace CLOUD.API.Helpers.ErrorHandeler
{
    public class ApiResponse
    {
        public int status { get; set; }
        public string Message { get; set; }

        public ApiResponse(int status, string message = null)
        {
            this.status = status;
            Message = message ?? handelError(status);
        }

        public static string handelError(int Status)
        => Status switch
        {
            400 => "Bad Request ,You Have Made",
            401 => "Authorized , You Are Not",
            404 => "Request Wasnt Found",
            500 => "Error Passed To The Dark Sied",
            _ => "Null"
        };
    }
}
