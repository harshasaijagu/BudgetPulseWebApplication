using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostgreSqlDotnetCore.Models;

namespace PostgreSqlDotnetCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Budget> budgets { get; set; }
    }
}