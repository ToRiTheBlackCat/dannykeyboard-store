﻿using DannyKeyboard.Application.DTOs.Password;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.OTP.Commands
{
    public class SendOTPCommand : IRequest<(bool,string)>
    {
        public string Email { get; set; }
        public SendOTPCommand(string email)
        {
            Email = email;
        }
    }
}
