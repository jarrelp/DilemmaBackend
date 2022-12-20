using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Auth.Commands;

public class AuthDto
{
    public string Token { get; set; } = null!;
    public ApplicationUserDto User { get; set; } = null!;
}
