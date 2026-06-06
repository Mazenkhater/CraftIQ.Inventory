using System.Net;

namespace CraftIQ.Inventory.Core.ResponseBases
{
    public class Response<T>
    {
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public object Meta { get; set; }
        public List<string> Errors { get; set; } // هستخدمها مع ال fluent validation عشان اعرض ال errors بتاعتها
        public Response() 
        {
        }
        public Response(T data, string message=null)
        {
            Data = data;
            Message = message;
            Succeeded = true;
        }
        public Response(string message)
        {
            Message = message;
            Succeeded = false;
        }
        public Response(string message,bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }

    }
}
