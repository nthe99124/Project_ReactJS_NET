using System.Collections.Generic;

namespace API.Common
{
    public class RestOutputCommand<T>
    {
        public RestOutputCommand()
        {

        }
        public bool Success { get; set; }
        public string Message { get; set; } = "Success";
        public List<T> Data { get; set; }
    }
}
