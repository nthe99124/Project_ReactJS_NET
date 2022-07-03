using System.Collections.Generic;

namespace API.Common
{
    public class RestOutputCommand<T>
    {
        public RestOutputCommand()
        {

        }

        public RestOutputCommand(T data)
        {
            Data = data;
        }
        public bool Success { get; set; }
        public string Message { get; set; } = "Success";
        private T Data { get; set; }
    }
}
