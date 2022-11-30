using System.Reflection;
using System.Reflection.Emit;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options, operationalStoreOptions)
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

        //many to many
        /*builder.Entity<OptionSkill>().HasKey(os => new { os.OptionId, os.SkillId });

        builder.Entity<OptionSkill>()
                    .HasOne(t => t.Option)
                    .WithMany(t => t.OptionSkills)
                    .HasForeignKey(t => t.OptionId);

        builder.Entity<OptionSkill>()
                    .HasOne(t => t.Skill)
                    .WithMany(t => t.OptionSkills)
                    .HasForeignKey(t => t.SkillId);*/

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
