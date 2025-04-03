﻿// <auto-generated />
using System;
using System.Collections.Generic;
using CourseService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseService.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Created_by")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Deleted_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Deleted_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

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

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

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
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("Instructor_Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsBolcked")
                        .HasColumnType("boolean");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MRP")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Course_Id");

                    b.HasIndex("Category_Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
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

                    b.Property<string>("DocumentUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("DocumentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Documents");
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

            modelBuilder.Entity("CourseService.Domain.Entities.VerifyCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

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
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.PrimitiveCollection<List<string>>("DocumentUrls")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("InstructorId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("Is_deleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MRP")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.PrimitiveCollection<List<string>>("VideoUrls")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("VerifyCourses");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Video", b =>
                {
                    b.Property<Guid>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Updated_by")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Updated_on")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("VideoId");

                    b.HasIndex("CourseId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Course", b =>
                {
                    b.HasOne("CourseService.Domain.Entities.Category", "Category")
                        .WithMany("Courses")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Document", b =>
                {
                    b.HasOne("CourseService.Domain.Entities.Course", null)
                        .WithMany("DocumentUrls")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Video", b =>
                {
                    b.HasOne("CourseService.Domain.Entities.Course", null)
                        .WithMany("VideoUrls")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Category", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("CourseService.Domain.Entities.Course", b =>
                {
                    b.Navigation("DocumentUrls");

                    b.Navigation("VideoUrls");
                });
#pragma warning restore 612, 618
        }
    }
}
