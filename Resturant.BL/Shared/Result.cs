using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Shared
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T> { IsSuccess = true, Data = data };
        }

        public static Result<T> Fail(string error)
        {
            return new Result<T> { IsSuccess = false, Error = error };
        }
    }
}
