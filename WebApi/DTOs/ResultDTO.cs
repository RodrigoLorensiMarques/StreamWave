using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class ResultDTO<T>
    {

        public ResultDTO(T data, string error)
        {
            Data = data;
            Errors.Add(error);
        }

        public ResultDTO(string error)
        {
            Errors.Add(error);
        }

        public ResultDTO(List<string> errors)
        {
            Errors = errors;
        }
        

        public ResultDTO(T data)
        {
            Data = data;
        }        


        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new List<string>();
    }
}