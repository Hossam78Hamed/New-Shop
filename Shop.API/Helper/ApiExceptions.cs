namespace Shop.API.Helper
{
    public class ApiExceptions : ResponseAPI
    {
        
        public ApiExceptions(int _StatusCode, string _Message = null,string _Details=null) : base(_StatusCode, _Message)
        {
            Details = _Details;
        }
        public string Details { get; set; }
    }
}
