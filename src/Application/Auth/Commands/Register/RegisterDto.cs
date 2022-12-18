using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Auth.Commands.Register;

public class RegisterDto : IMapFrom<ApplicationUser>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get ; set; } = null!;
    public string DepartmentId { get; set; } = null!;
}
