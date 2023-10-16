using System.Net;

namespace WebApplication1.Models
{
    public class ApiResponse
    {

        public ApiResponse() {
            Errors1 = new List<string>();
        }
        public bool Success { get; set; }
        public Object Result { get; set; }

        public HttpStatusCode httpStatusCode { get; set; }

        public List <string> Errors1 { get; set; }


    }
}
