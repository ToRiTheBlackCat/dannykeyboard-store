using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Password
{
    public class ResponseForgotPasswordDto
    {
        public bool IsSend {  get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
