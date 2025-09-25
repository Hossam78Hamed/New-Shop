namespace Shop.API.Helper
{
    public class ResponseAPI
    {
       public int StatusCode { get; set; }
        public string Message { get; set; }
     
        public ResponseAPI(int _StatusCode,string _Message=null) {
        StatusCode= _StatusCode;
        Message=_Message??GetMessageFormStatusCode(_StatusCode);
        
        }
        public string GetMessageFormStatusCode(int _StatusCode) {
            return _StatusCode switch
            {
                200 => "OK",
                201 => "Created",
                204 => "No Content",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status Code"
            };

        }

    }
}
