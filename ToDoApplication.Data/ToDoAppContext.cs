using Microsoft.EntityFrameworkCore;
using ToDoApplication.Domain;

namespace ToDoApplication.Data
{
    public class ToDoAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; } 
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UsersToDos> UsersToDos { get; set; }
        public DbSet<UsersTasks> UsersTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany(_ => _.Lists)
                .WithMany(_ => _.Users)
                .UsingEntity<UsersToDos>(
                _ => _
                .HasOne(_ => _.ToDoList)
                .WithMany(_ => _.UsersToDos)
                .HasForeignKey(_ => _.ID),
                _ => _
                .HasOne(_ => _.User)
                .WithMany(_ => _.UsersToDos)
                .HasForeignKey(_ => _.ID));

            modelBuilder.Entity<UsersToDos>()
                .HasKey(_ => new { _.ListsID, _.UsersID });

            modelBuilder
               .Entity<User>()
               .HasMany(_ => _.Tasks)
               .WithMany(_ => _.Users)
               .UsingEntity<UsersTasks>(
               _ => _
               .HasOne(_ => _.Task)
               .WithMany(_ => _.UsersTasks)
               .HasForeignKey(_ => _.ID),
               _ => _
               .HasOne(_ => _.User)
               .WithMany(_ => _.UsersTasks)
               .HasForeignKey(_ => _.ID));
          
            modelBuilder.Entity<ToDoList>()
                 .HasMany(_ => _.Tasks)
                 .WithOne()
                 .HasForeignKey(_ => _.ListId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(@"Data Source=localhost;Initial Catalog=TestToDoDB;Integrated Security=True");
        }
    }
}
