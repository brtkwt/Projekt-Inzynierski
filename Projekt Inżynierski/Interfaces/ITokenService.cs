using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projekt_Inżynierski.Entities;

namespace Projekt_Inżynierski.Interfaces
{
    public interface ITokenService
    {
        string CreateJWTToken(AppUser appUser);
    }
}