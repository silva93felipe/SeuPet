using System.Net;

namespace SeuPet.Api.Dto
{
    public class ResponseHttp{
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public ResponseHttp(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
            Errors = new List<string>();
        }

        public ResponseHttp(HttpStatusCode statusCode, bool success, object data) : this(statusCode, success) => Data = data;
        public ResponseHttp(HttpStatusCode statusCode, bool success, IEnumerable<string> erros) : this(statusCode, success) => Errors = erros;
        public ResponseHttp(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> erros) : this(statusCode, success, data) => Errors = erros;
    }
}