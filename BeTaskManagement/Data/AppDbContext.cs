using BeTaskManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeTaskManagement.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BeTask> BeTasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CommentHistory> CommentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeTask>()
                .Property(t => t.BeTaskId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Comment>()
                .Property(c => c.CommentId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<BeTask>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<BeTask>()
                .Property(t => t.Type)
                .HasConversion<string>();

            modelBuilder.Entity<Comment>()
            .Property(c => c.CommentType)
            .HasConversion<string>();

            modelBuilder.Entity<BeTask>()
                .HasOne(t => t.AssignedTo)
                .WithMany(u => u.BeTasks)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.BeTask)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentHistory>()
                .HasOne(ch => ch.Comment)
                .WithMany(c => c.History)
                .HasForeignKey(ch => ch.CommentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
