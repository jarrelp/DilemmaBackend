using System.Diagnostics;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // department
        var d1 = new Department { Name = "Department 1" };
        var d2 = new Department { Name = "Department 2" };

        _context.Departments.AddRange(d1, d2);

        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "test", Email = "test@localhost", Department = d1 };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Test1!");
            await _userManager.AddToRolesAsync(administrator, new List<string>() { administratorRole.Name });
        }

        // Default data
        // Seed, if necessary
        if (!_context.Questions.Any())
        {
            //skill
            var s1 = new Skill { Name = "Skill 1" };
            var s2 = new Skill { Name = "Skill 2" };
            var s3 = new Skill { Name = "Skill 3" };
            var s4 = new Skill { Name = "Skill 4" };
            var s5 = new Skill { Name = "Skill 5" };
            var s6 = new Skill { Name = "Skill 6" };

            _context.Skills.AddRange(s1, s2, s3, s4, s5, s6);

            //optionskill
            var os1 = new OptionSkill
            {
                SkillId = 1,
                SkillLevel = SkillLevel.Low
            };
            var os2 = new OptionSkill
            {
                SkillId = 2,
                SkillLevel = SkillLevel.Medium
            };
            var os3 = new OptionSkill
            {
                SkillId = 3,
                SkillLevel = SkillLevel.Low
            };
            var os4 = new OptionSkill
            {
                SkillId = 4,
                SkillLevel = SkillLevel.Medium
            };
            var os5 = new OptionSkill
            {
                SkillId = 5,
                SkillLevel = SkillLevel.Low
            };
            var os6 = new OptionSkill
            {
                SkillId = 6,
                SkillLevel = SkillLevel.Medium
            };

            //option
            var o1 = new Option { Description = "option 1: 📃", OptionSkills = new List<OptionSkill>() { os1, os2 } };
            var o2 = new Option { Description = "option 2: ✅", OptionSkills = new List<OptionSkill>() { os3, os4 } };
            var o3 = new Option { Description = "option 3: 🤯", OptionSkills = new List<OptionSkill>() { os5, os6 } };
            var options = new List<Option>() { o1, o2, o3 };

            _context.Options.AddRange(o1, o2, o3);

            //question
            var question1 = new Question { Description = "Question", Options = options };
            _context.Questions.Add(question1);

            //quiz
            var quiz1 = new Quiz { Description = "Quiz", Active = true, Questions = new List<Question>() { question1 } };
            _context.Quizzes.Add(quiz1);

            //result
            var result1 = new Result { Answers = new List<Option>() { o1 }, Quiz = quiz1, ApplicationUser = administrator, ApplicationUserId = administrator.Id };
            _context.Results.Add(result1);

            /*administrator.Results.Add(result1);

            await _userManager.UpdateAsync(administrator);*/

            await _context.SaveChangesAsync();
        }
    }
}