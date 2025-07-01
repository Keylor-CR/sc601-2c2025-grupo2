using System;

namespace SinpeEmpresarial.Shared.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseModel()
        {
            Success = false;
            Message = string.Empty;
        }

        public ResponseModel(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public ResponseModel(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
