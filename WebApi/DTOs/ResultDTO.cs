using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTOs
{
    public class ResultDTO<T>
    {

        public ResultDTO(T data, string message)
        {
            Data = data;
            Messages.Add(message);
        }

        public ResultDTO(string message)
        {
            Messages.Add(message);
        }

        public ResultDTO(List<string> messages)
        {
            Messages = messages;
        }
        

        public ResultDTO(T data)
        {
            Data = data;
        }        


        public T Data { get; private set; }
        public List<string> Messages { get; private set; } = new List<string>();
    }
}