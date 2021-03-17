using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDocumentManagement.Models
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext()
        {
        }

        public DocumentDbContext(DbContextOptions<DocumentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasOne<Document>(doc => doc.Document)
            .WithOne(doc => doc.User)
            .HasForeignKey<Document>(doc => doc.UserId);
        }
        public DbSet<User> User { get; set; }
        public DbSet<Document> Document { get; set; }
    }
}
