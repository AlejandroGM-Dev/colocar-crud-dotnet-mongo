using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColocarCrud.Application.Users
{
    public record UpdateUserDto(string Id, string Email, string FullName);
}
