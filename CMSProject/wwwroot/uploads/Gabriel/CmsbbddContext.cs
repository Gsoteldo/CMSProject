using System;
using System.Collections.Generic;
using CMSProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CMSProject.Data;

public partial class CmsbbddContext : DbContext
{
    public CmsbbddContext()
    {
    }

    public CmsbbddContext(DbContextOptions<CmsbbddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileTable> FileTables { get; set; }

    public virtual DbSet<PublicationTable> PublicationTables { get; set; }

    public virtual DbSet<RoleTable> RoleTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileTable>(entity =>
        {
            entity.ToTable("FileTable");

            entity.HasIndex(e => e.IdPublication, "IX_FileTable_IdPublication");

            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Path)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.FileTables)
                .HasForeignKey(d => d.IdPublication)
                .HasConstraintName("FK_FileTable_PublicationTable");
        });

        modelBuilder.Entity<PublicationTable>(entity =>
        {
            entity.ToTable("PublicationTable");

            entity.HasIndex(e => e.IdUser, "IX_PublicationTable_IdUser");

            entity.Property(e => e.Concept)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsFixedLength();

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PublicationTables)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_PublicationTable_UserTable");
        });

        modelBuilder.Entity<RoleTable>(entity =>
        {
            entity.ToTable("RoleTable");

            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .IsFixedLength();
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.ToTable("UserTable");

            entity.HasIndex(e => e.IdRole, "IX_UserTable_IdRole");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasDefaultValue("")
                .IsFixedLength();

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.InverseIdClientNavigation)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_UserTable_UserTable");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_UserTable_RoleTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
