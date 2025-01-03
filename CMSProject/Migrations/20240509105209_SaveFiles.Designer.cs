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
    [Migration("20240509105209_SaveFiles")]
    partial class SaveFiles
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

                    b.Property<int?>("IdPublication")
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

                    b.HasIndex(new[] { "IdPublication" }, "IX_FileTable_IdPublication");

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
                        .HasMaxLength(200)
                        .HasColumnType("nchar(200)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdUser" }, "IX_PublicationTable_IdUser");

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
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .HasDefaultValue("")
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
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasDefaultValue("")
                        .IsFixedLength();

                    b.Property<int?>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasDefaultValue("")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("nchar(500)")
                        .HasDefaultValue("")
                        .IsFixedLength();

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex(new[] { "IdRole" }, "IX_UserTable_IdRole");

                    b.ToTable("UserTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.FileTable", b =>
                {
                    b.HasOne("CMSProject.Models.PublicationTable", "IdPublicationNavigation")
                        .WithMany("FileTables")
                        .HasForeignKey("IdPublication")
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
                    b.HasOne("CMSProject.Models.UserTable", "IdClientNavigation")
                        .WithMany("InverseIdClientNavigation")
                        .HasForeignKey("IdClient")
                        .HasConstraintName("FK_UserTable_UserTable");

                    b.HasOne("CMSProject.Models.RoleTable", "IdRoleNavigation")
                        .WithMany("UserTables")
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_UserTable_RoleTable");

                    b.Navigation("IdClientNavigation");

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
                    b.Navigation("InverseIdClientNavigation");

                    b.Navigation("PublicationTables");
                });
#pragma warning restore 612, 618
        }
    }
}
