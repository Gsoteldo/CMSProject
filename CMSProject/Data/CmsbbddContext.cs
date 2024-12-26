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

    public virtual DbSet<LastModificationTable> LastModificationTables { get; set; }

    public virtual DbSet<PublicationTable> PublicationTables { get; set; }

    public virtual DbSet<RoleTable> RoleTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    public virtual DbSet<PostingTable> PostingTables { get; set; }

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
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.IdPublication).HasDefaultValue(0);
            entity.Property(e => e.Path)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdPublicationNavigation).WithMany(p => p.FileTables)
                .HasForeignKey(d => d.IdPublication)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FileTable_PublicationTable");
        });

        modelBuilder.Entity<LastModificationTable>(entity =>
        {
            entity.ToTable("LastModificationTable");

            entity.Property(e => e.LastModification).HasColumnType("datetime");
            entity.Property(e => e.Modification)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.IdCreatorNavigation).WithMany(p => p.LastModificationTables)
                .HasForeignKey(d => d.IdCreator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LastModificationTable_UserTable1");
        });

        modelBuilder.Entity<PublicationTable>(entity =>
        {
            entity.ToTable("PublicationTable");

            entity.HasIndex(e => e.IdUser, "IX_PublicationTable_IdUser");

            entity.Property(e => e.Concept)
                .HasMaxLength(int.MaxValue)
                .IsFixedLength();
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsFixedLength();

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.PublicationTables)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
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

            entity.HasIndex(e => e.IdClient, "IX_UserTable_IdClient");

            entity.HasIndex(e => e.IdRole, "IX_UserTable_IdRole");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
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
