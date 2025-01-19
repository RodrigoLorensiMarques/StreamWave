using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class Result<T>
    {

        public Result(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public Result(string errors)
        {
            Errors.Add(errors);
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
    }
}