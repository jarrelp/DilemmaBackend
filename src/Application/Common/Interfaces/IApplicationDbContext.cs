using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Question> Questions { get; }

    DbSet<Option> Options { get; }

    DbSet<Skill> Skills { get; }

    DbSet<OptionSkill> OptionSkills { get; }

    DbSet<Department> Departments { get; }

    DbSet<Result> Results { get; }

    DbSet<Quiz> Quizzes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
