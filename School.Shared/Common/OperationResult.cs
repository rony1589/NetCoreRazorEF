﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Shared.Common
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public static OperationResult Ok(string? message = null)
            => new OperationResult { Success = true, Message = message };

        public static OperationResult Fail(string message)
            => new OperationResult { Success = false, Message = message };
    }

    public class OperationResult<T> : OperationResult
    { 
        public T? Data { get; set; }
        public static OperationResult<T> Ok(T data, string? message = null)
            => new OperationResult<T> { Success = true, Data = data, Message = message };

        public static new OperationResult<T> Fail(string message)
           => new OperationResult<T> { Success = false, Message = message };

    }
}
