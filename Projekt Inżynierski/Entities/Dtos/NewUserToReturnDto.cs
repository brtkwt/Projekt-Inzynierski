using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Inżynierski.Entities.Dtos
{
    public class NewUserToReturnDto
    {
        public string GivenName { get; set; }
        public string EmailAdress { get; set; }
        public string Token { get; set; }
    }
}