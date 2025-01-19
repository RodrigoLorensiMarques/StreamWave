using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class ResultDTO<T>
    {

        public ResultDTO(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultDTO(string errors)
        {
            Errors.Add(errors);
        }


        public ResultDTO(T data)
        {
            Data = data;
        }        

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
    }
}