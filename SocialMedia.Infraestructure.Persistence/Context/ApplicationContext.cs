using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Application.Dtos.Account;
using SocialMedia.Core.Application.Helpers;

using SocialMedia.Core.Domain.Common;
using SocialMedia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created=DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            #endregion

            #region Primary key
            modelBuilder.Entity<Post>().HasKey(entity => entity.Id);
            modelBuilder.Entity<Comment>().HasKey(entity => entity.Id);

            #endregion

            #region Relationships
            modelBuilder.Entity<Post>()
                .HasMany<Comment>(Post=>Post.Comments)
                .WithOne(comment => comment.Post)
                .HasForeignKey(comment => comment.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Properties
            #region Post
                modelBuilder.Entity<Post>()
                    .Property(p => p.VisualContentPath)
                    .HasMaxLength(200); 
                modelBuilder.Entity<Post>()
                    .Property(p => p.Descripcion)
                    .HasMaxLength(500);
                modelBuilder.Entity<Post>()
                    .Property(p => p.VisualContentType)
                    .HasMaxLength(50);
                modelBuilder.Entity<Post>()
                    .Property(p => p.UserId)
                    .IsRequired()
                    .HasMaxLength(200);
            #endregion
            #region Comment
                modelBuilder.Entity<Comment>()
                    .Property(p => p.Message)
                    .IsRequired()
                    .HasMaxLength(500);
            modelBuilder.Entity<Comment>()
                    .Property(p => p.CommentType)
                    .IsRequired()
                    .HasMaxLength(50);
            modelBuilder.Entity<Comment>()
                    .Property(p => p.UserId)
                    .IsRequired()
                    .HasMaxLength(200);
            #endregion
            #endregion
        }
    }
}
