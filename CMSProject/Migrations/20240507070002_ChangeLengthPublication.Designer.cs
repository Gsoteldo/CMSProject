﻿// <auto-generated />
using System;
using CMSProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMSProject.Migrations
{
    [DbContext(typeof(CmsbbddContext))]
    [Migration("20240507070002_ChangeLengthPublication")]
    partial class ChangeLengthPublication
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CMSProject.Models.FileTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileName")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<int>("IdPublication")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .HasMaxLength(1000)
                        .HasColumnType("nchar(1000)")
                        .IsFixedLength();

                    b.Property<string>("Type")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("IdPublication");

                    b.ToTable("FileTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.PublicationTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Concept")
                        .HasMaxLength(1000)
                        .HasColumnType("nchar(1000)")
                        .IsFixedLength();

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("PublicationDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.ToTable("PublicationTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.RoleTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("RoleTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.UserTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRole");

                    b.ToTable("UserTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.FileTable", b =>
                {
                    b.HasOne("CMSProject.Models.PublicationTable", "IdPublicationNavigation")
                        .WithMany("FileTables")
                        .HasForeignKey("IdPublication")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_FileTable_PublicationTable");

                    b.Navigation("IdPublicationNavigation");
                });

            modelBuilder.Entity("CMSProject.Models.PublicationTable", b =>
                {
                    b.HasOne("CMSProject.Models.UserTable", "IdUserNavigation")
                        .WithMany("PublicationTables")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_PublicationTable_UserTable");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("CMSProject.Models.UserTable", b =>
                {
                    b.HasOne("CMSProject.Models.RoleTable", "IdRoleNavigation")
                        .WithMany("UserTables")
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTable_RoleTable");

                    b.Navigation("IdRoleNavigation");
                });

            modelBuilder.Entity("CMSProject.Models.PublicationTable", b =>
                {
                    b.Navigation("FileTables");
                });

            modelBuilder.Entity("CMSProject.Models.RoleTable", b =>
                {
                    b.Navigation("UserTables");
                });

            modelBuilder.Entity("CMSProject.Models.UserTable", b =>
                {
                    b.Navigation("PublicationTables");
                });
#pragma warning restore 612, 618
        }
    }
}