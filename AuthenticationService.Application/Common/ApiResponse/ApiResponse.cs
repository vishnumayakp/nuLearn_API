﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Application.Common.ApiResponse
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }
        public ApiResponse(int statusCode, string? message, T? data = default(T), string? error = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Error = error;
        }
    }
}
