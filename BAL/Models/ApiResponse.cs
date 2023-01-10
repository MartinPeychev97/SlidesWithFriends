using System.Collections.Generic;

namespace BAL.Models
{
    public class ApiResponse<T>
    {
        public bool Error { get; set; }

        public string Msg { get; set; }

        public IEnumerable<T> Data { get; set; }

    }
}
