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
    [Migration("20240521152236_AddTinyMCE")]
    partial class AddTinyMCE
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
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<int?>("IdPublication")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Path")
                        .HasMaxLength(1000)
                        .HasColumnType("nchar(1000)")
                        .IsFixedLength();

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdPublication" }, "IX_FileTable_IdPublication");

                    b.ToTable("FileTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.LastModificationTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("IdCreator")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime");

                    b.Property<string>("Modification")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("Publication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCreator");

                    b.ToTable("LastModificationTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.PostingTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FileId")
                        .HasColumnType("int");

                    b.Property<int>("PublicationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.HasIndex("PublicationId");

                    b.ToTable("PostingTables");
                });

            modelBuilder.Entity("CMSProject.Models.PublicationTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Concept")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("nchar(2147483647)")
                        .IsFixedLength();

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<bool?>("IsPost")
                        .HasColumnType("bit");

                    b.Property<DateOnly?>("PublicationDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
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
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .HasDefaultValue("")
                        .IsFixedLength();

                    b.Property<int?>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
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

                    b.HasIndex(new[] { "IdClient" }, "IX_UserTable_IdClient");

                    b.HasIndex(new[] { "IdRole" }, "IX_UserTable_IdRole");

                    b.ToTable("UserTable", (string)null);
                });

            modelBuilder.Entity("CMSProject.Models.FileTable", b =>
                {
                    b.HasOne("CMSProject.Models.PublicationTable", "IdPublicationNavigation")
                        .WithMany("FileTables")
                        .HasForeignKey("IdPublication")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_FileTable_PublicationTable");

                    b.Navigation("IdPublicationNavigation");
                });

            modelBuilder.Entity("CMSProject.Models.LastModificationTable", b =>
                {
                    b.HasOne("CMSProject.Models.UserTable", "IdCreatorNavigation")
                        .WithMany("LastModificationTables")
                        .HasForeignKey("IdCreator")
                        .HasConstraintName("FK_LastModificationTable_UserTable1");

                    b.Navigation("IdCreatorNavigation");
                });

            modelBuilder.Entity("CMSProject.Models.PostingTable", b =>
                {
                    b.HasOne("CMSProject.Models.FileTable", "File")
                        .WithMany()
                        .HasForeignKey("FileId");

                    b.HasOne("CMSProject.Models.PublicationTable", "Publication")
                        .WithMany()
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");

                    b.Navigation("Publication");
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

                    b.Navigation("LastModificationTables");

                    b.Navigation("PublicationTables");
                });
#pragma warning restore 612, 618
        }
    }
}
