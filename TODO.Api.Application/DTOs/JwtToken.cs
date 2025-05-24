using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.Api.Application.DTOs
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public JwtToken(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }
        public JwtToken() { }
    }
}
