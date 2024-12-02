using System.Net;

namespace SeuPet.Dto
{
    public class ResponseHtttp{
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public ResponseHtttp(HttpStatusCode statusCode, bool success)
        {
            StatusCode = statusCode;
            Success = success;
        }

        public ResponseHtttp(HttpStatusCode statusCode, bool success, object data) : this(statusCode, success) => Data = data;
        public ResponseHtttp(HttpStatusCode statusCode, bool success, IEnumerable<string> erros) : this(statusCode, success) => Errors = erros;
        public ResponseHtttp(HttpStatusCode statusCode, bool success, object data, IEnumerable<string> erros) : this(statusCode, success, data) => Errors = erros;
    }
}