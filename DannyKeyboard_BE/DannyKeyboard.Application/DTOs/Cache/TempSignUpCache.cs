using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.DTOs.Cache
{
    public class TempSignUpCache
    {
        public string OtpCode { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
    }
}
