﻿using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class DepartmentDto : IMapFrom<Department>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
