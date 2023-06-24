using API.Model;
using API.Utilities;
using Microsoft.EntityFrameworkCore;
/*using File = API.Model.File;*/

namespace API.Contracts;

public class ProjectManagementDBContext : DbContext
{
    public ProjectManagementDBContext(DbContextOptions<ProjectManagementDBContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Model.Task> Tasks { get; set; }
    public DbSet<Rating> Ratings { get; set; }
/*    public DbSet<File> Files { get; set; }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(new Role
        {
            Guid = Guid.Parse("BAD2010A-8D51-4EAF-ECCB-08DB73D114FF"),
            Name = nameof(RoleLevel.employee),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        }, new Role
        {
            Guid = Guid.Parse("F0ED952A-0321-4193-3653-08DB73D30B74"),
            Name = nameof(RoleLevel.manager),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        });

        modelBuilder.Entity<Employee>().HasIndex(e => new
        {
            e.NIK,
            e.Email,
            e.PhoneNumber
        }).IsUnique();

        // 1 to 1 task with rating
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Task)
            .WithOne(t => t.Rating)
            .HasForeignKey<Rating>(r => r.Guid);

        // 1 to 1 task with report
        modelBuilder.Entity<Report>()
            .HasOne(r => r.Task)
            .WithOne(t => t.Report)
            .HasForeignKey<Report>(t => t.Guid);
    
        // 1 to 1 report with file
/*        modelBuilder.Entity<File>()
            .HasOne(f => f.Report)
            .WithOne(r => r.File)
            .HasForeignKey<File>(f => f.Guid);  
*/
        // 1 to many task with employee
        modelBuilder.Entity<Model.Task>()
            .HasOne(t => t.Employee)
            .WithMany(e => e.Task)
            .HasForeignKey(t => t.EmployeeGuid);

        // 1 to many (self-referencing table) 
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany(e => e.Subordinates)
            .HasForeignKey(e => e.ManagerID);
        
        // 1 to 1 employee with account
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Employee)
            .WithOne(e => e.Account)
            .HasForeignKey<Account>(a => a.Guid);

        // 1 to many account with accountrole
        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Account)
            .WithMany(a => a.AccountRole)
            .HasForeignKey(ar => ar.AccountGuid);

        // 1 to many accountrole with role
        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AccountRole)
            .HasForeignKey(ar => ar.RoleGuid);
    }
}
