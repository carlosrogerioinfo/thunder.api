using Microsoft.EntityFrameworkCore;
using Thunder.Project.Domain.Entities;
using Thunder.Project.Infrastructure.Mappings;

namespace Thunder.Project.Infrastructure.Contexts
{
    public class TodoDataContext : DbContext
    {
        public TodoDataContext() { }

        public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //SQL Server 
            //options.UseSqlServer("Server=appdbtest.mssql.somee.com;Database=appdbtest;User ID=carlos_mdb_SQLLogin_1;Password=wxgrh515lh;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoMap());
        }
    }
}