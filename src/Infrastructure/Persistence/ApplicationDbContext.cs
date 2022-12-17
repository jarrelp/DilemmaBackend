using System.Reflection;
using System.Reflection.Emit;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Question> Questions => Set<Question>();

    public DbSet<Option> Options => Set<Option>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<OptionSkill> OptionSkills => Set<OptionSkill>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Result> Results => Set<Result>();

    public DbSet<Quiz> Quizzes => Set<Quiz>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //one to many
        builder.Entity<Department>()
            .HasMany(c => c.ApplicationUsers)
            .WithOne(e => e.Department)
            .OnDelete(DeleteBehavior.SetNull);

        /*builder.Entity<Quiz>()
            .HasMany(c => c.Questions)
            .WithOne(c => c.Quiz)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Question>()
            .HasMany(c => c.Options)
            .WithOne(c => c.Question)
            .OnDelete(DeleteBehavior.Cascade);*/

        //many to many
        builder.Entity<OptionSkill>().HasKey(os => new { os.OptionId, os.SkillId });

        builder.Entity<OptionSkill>()
                    .HasOne(t => t.Option)
                    .WithMany(t => t.OptionSkills)
                    .HasForeignKey(t => t.OptionId)
                    .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<OptionSkill>()
                    .HasOne(t => t.Skill)
                    .WithMany(t => t.OptionSkills)
                    .HasForeignKey(t => t.SkillId)
                    .OnDelete(DeleteBehavior.Cascade);

        //many to many
        builder.Entity<Result>().HasKey(os => new { os.ApplicationUserId, os.QuizId });

        builder.Entity<Result>()
                    .HasOne(t => t.ApplicationUser)
                    .WithMany(t => t.Results)
                    .HasForeignKey(t => t.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Result>()
                    .HasOne(t => t.Quiz)
                    .WithMany(t => t.Results)
                    .HasForeignKey(t => t.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);

        /*// one to many
        builder.Entity<Result>()
            .HasMany(c => c.Answers)
            .WithOne(c => c.Result)
            .OnDelete(DeleteBehavior.Cascade);*/

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /*optionsBuilder
        .LogTo(Console.WriteLine, new[] { InMemoryEventId.ChangesSaved })
        .UseInMemoryDatabase("UserContextWithNullCheckingDisabled", b => b.EnableNullChecks(false));*/
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
