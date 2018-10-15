﻿// <auto-generated />
using System;
using Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EbookShop.Migrations
{
    [DbContext(typeof(EbookShopContext))]
    [Migration("20181014212111_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.DataModels.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Core.DataModels.AuthorEbooks", b =>
                {
                    b.Property<int>("AuthorId");

                    b.Property<int>("EbookId");

                    b.HasKey("AuthorId", "EbookId");

                    b.HasIndex("EbookId");

                    b.ToTable("AuthorEbooks");
                });

            modelBuilder.Entity("Core.DataModels.Ebook", b =>
                {
                    b.Property<int>("EbookId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(13);

                    b.Property<decimal>("Price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("EbookId");

                    b.ToTable("Ebooks");
                });

            modelBuilder.Entity("Core.DataModels.FilePath", b =>
                {
                    b.Property<int>("FilePathId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EbookId");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("FileType");

                    b.Property<string>("Path");

                    b.HasKey("FilePathId");

                    b.HasIndex("EbookId");

                    b.ToTable("FilePath");
                });

            modelBuilder.Entity("Core.DataModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FB_Token");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(101);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.DataModels.AuthorEbooks", b =>
                {
                    b.HasOne("Core.DataModels.Author", "Author")
                        .WithMany("AuthorEbooks")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.DataModels.Ebook", "Ebook")
                        .WithMany("AuthorEbooks")
                        .HasForeignKey("EbookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.DataModels.FilePath", b =>
                {
                    b.HasOne("Core.DataModels.Ebook")
                        .WithMany("Files")
                        .HasForeignKey("EbookId");
                });
#pragma warning restore 612, 618
        }
    }
}
