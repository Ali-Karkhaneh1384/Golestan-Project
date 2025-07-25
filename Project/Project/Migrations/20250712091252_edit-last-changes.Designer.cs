﻿// <auto-generated />
using System;
using Golestan_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(GolestanDbContext))]
    [Migration("20250712091252_edit-last-changes")]
    partial class editlastchanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Models.classrooms", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("building")
                        .IsRequired()
                        .HasColumnType("varchar(225)");

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.Property<int>("room_number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("classrooms");
                });

            modelBuilder.Entity("Project.Models.courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(225)");

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("varchar(225)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("varchar(225)");

                    b.Property<int>("unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("Project.Models.instructors", b =>
                {
                    b.Property<int>("instructor_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("instructor_id"));

                    b.Property<DateTime>("hire_date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("salary")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("instructor_id");

                    b.HasIndex("user_id");

                    b.ToTable("instructors");
                });

            modelBuilder.Entity("Project.Models.roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            name = 0
                        },
                        new
                        {
                            Id = 2,
                            name = 2
                        },
                        new
                        {
                            Id = 3,
                            name = 1
                        });
                });

            modelBuilder.Entity("Project.Models.sections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("classroom_id")
                        .HasColumnType("int");

                    b.Property<int>("course_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("final_exam_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("semester")
                        .HasColumnType("int");

                    b.Property<int>("time_slot_id")
                        .HasColumnType("int");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("classroom_id");

                    b.HasIndex("course_id");

                    b.HasIndex("time_slot_id");

                    b.ToTable("sections");
                });

            modelBuilder.Entity("Project.Models.students", b =>
                {
                    b.Property<int>("student_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("student_id"));

                    b.Property<DateTime>("enrollment_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("student_id");

                    b.HasIndex("user_id");

                    b.ToTable("students");
                });

            modelBuilder.Entity("Project.Models.takes", b =>
                {
                    b.Property<int>("student_id")
                        .HasColumnType("int");

                    b.Property<int>("section_id")
                        .HasColumnType("int");

                    b.Property<int>("grade")
                        .HasColumnType("int");

                    b.HasKey("student_id", "section_id");

                    b.HasIndex("section_id");

                    b.ToTable("takes");
                });

            modelBuilder.Entity("Project.Models.teach", b =>
                {
                    b.Property<int>("instructor_id")
                        .HasColumnType("int");

                    b.Property<int>("section_id")
                        .HasColumnType("int");

                    b.HasKey("instructor_id", "section_id");

                    b.HasIndex("section_id")
                        .IsUnique();

                    b.ToTable("teaches");
                });

            modelBuilder.Entity("Project.Models.time_slots", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("day")
                        .IsRequired()
                        .HasColumnType("varchar(225)");

                    b.Property<DateTime>("end_time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("start_time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("timeslots");
                });

            modelBuilder.Entity("Project.Models.user_roles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("user_roles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Project.Models.users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar (50)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("varchar (50)");

                    b.Property<string>("Hashed_Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasColumnType("varchar (50)");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created_at = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@example.com",
                            First_Name = "Admin",
                            Hashed_Password = "Admin@1234",
                            Last_Name = "User"
                        });
                });

            modelBuilder.Entity("Project.Models.instructors", b =>
                {
                    b.HasOne("Project.Models.users", "user")
                        .WithMany("Instructors")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("Project.Models.sections", b =>
                {
                    b.HasOne("Project.Models.classrooms", "classroom")
                        .WithMany("sections")
                        .HasForeignKey("classroom_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.courses", "course")
                        .WithMany("sections")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.time_slots", "time_slot")
                        .WithMany("sections")
                        .HasForeignKey("time_slot_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("classroom");

                    b.Navigation("course");

                    b.Navigation("time_slot");
                });

            modelBuilder.Entity("Project.Models.students", b =>
                {
                    b.HasOne("Project.Models.users", "users")
                        .WithMany("students")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("users");
                });

            modelBuilder.Entity("Project.Models.takes", b =>
                {
                    b.HasOne("Project.Models.sections", "sections")
                        .WithMany("takes")
                        .HasForeignKey("section_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.students", "students")
                        .WithMany("takes")
                        .HasForeignKey("student_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("sections");

                    b.Navigation("students");
                });

            modelBuilder.Entity("Project.Models.teach", b =>
                {
                    b.HasOne("Project.Models.instructors", "instructor")
                        .WithMany("teaches")
                        .HasForeignKey("instructor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.sections", "section")
                        .WithOne("teach")
                        .HasForeignKey("Project.Models.teach", "section_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("instructor");

                    b.Navigation("section");
                });

            modelBuilder.Entity("Project.Models.user_roles", b =>
                {
                    b.HasOne("Project.Models.roles", "roles")
                        .WithMany("user_roles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.users", "users")
                        .WithMany("user_roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("roles");

                    b.Navigation("users");
                });

            modelBuilder.Entity("Project.Models.classrooms", b =>
                {
                    b.Navigation("sections");
                });

            modelBuilder.Entity("Project.Models.courses", b =>
                {
                    b.Navigation("sections");
                });

            modelBuilder.Entity("Project.Models.instructors", b =>
                {
                    b.Navigation("teaches");
                });

            modelBuilder.Entity("Project.Models.roles", b =>
                {
                    b.Navigation("user_roles");
                });

            modelBuilder.Entity("Project.Models.sections", b =>
                {
                    b.Navigation("takes");

                    b.Navigation("teach")
                        .IsRequired();
                });

            modelBuilder.Entity("Project.Models.students", b =>
                {
                    b.Navigation("takes");
                });

            modelBuilder.Entity("Project.Models.time_slots", b =>
                {
                    b.Navigation("sections");
                });

            modelBuilder.Entity("Project.Models.users", b =>
                {
                    b.Navigation("Instructors");

                    b.Navigation("students");

                    b.Navigation("user_roles");
                });
#pragma warning restore 612, 618
        }
    }
}
