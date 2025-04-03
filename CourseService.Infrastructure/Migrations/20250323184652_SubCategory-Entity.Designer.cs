﻿// <auto-generated />
using System;
using CourseService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250323184652_SubCategory-Entity")]
    partial class SubCategoryEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseService.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Created_by")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Deleted_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Deleted_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Course_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Category_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Course_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Created_by")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Deleted_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Deleted_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Instructor_Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("Is_Bolcked")
                        .HasColumnType("boolean");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MRP")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("SubCategory_Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Course_Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.SubCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Created_by")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Deleted_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Deleted_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("SubCategory_Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
