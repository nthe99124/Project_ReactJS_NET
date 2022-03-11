using System.Collections.Generic;

namespace API.Common
{
    public class RestOutput<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }

    }
}
