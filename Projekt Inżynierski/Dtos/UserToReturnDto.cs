using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Dtos
{
    public class UserToReturnDto
    {
        public string Token { get; set; }
        public string GivenName { get; set; }
        public string EmailAddress { get; set; }
    }
}