﻿// <auto-generated />
using System;
using DiplomaThesisDigitalization.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiplomaThesisDigitalization.Migrations
{
    [DbContext(typeof(ThesisDbContext))]
    [Migration("20240126173729_Intial")]
    partial class Intial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentMentor", b =>
                {
                    b.Property<int>("DepartmentsId")
                        .HasColumnType("int");

                    b.Property<int>("MentorsId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsId", "MentorsId");

                    b.HasIndex("MentorsId");

                    b.ToTable("DepartmentMentor");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.ConsultationSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ConsultationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConsultationPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiplomaThesisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiplomaThesisId");

                    b.ToTable("ConsultationSchedules");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FacultyId")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.DiplomaThesis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte?>("Assessment")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TitleID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MentorId");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.HasIndex("TitleID");

                    b.ToTable("DiplomaTheses");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Mentor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Availability")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("DegreeLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("DiplomaThesisId")
                        .HasColumnType("int");

                    b.Property<int>("ECTS")
                        .HasColumnType("int");

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("FieldId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FieldId")
                        .HasColumnType("int");

                    b.Property<string>("TitleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FieldMentor", b =>
                {
                    b.Property<int>("FieldsId")
                        .HasColumnType("int");

                    b.Property<int>("MentorsId")
                        .HasColumnType("int");

                    b.HasKey("FieldsId", "MentorsId");

                    b.HasIndex("MentorsId");

                    b.ToTable("FieldMentor");
                });

            modelBuilder.Entity("DepartmentMentor", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Mentor", null)
                        .WithMany()
                        .HasForeignKey("MentorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Administrator", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.ConsultationSchedule", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.DiplomaThesis", "DiplomaThesis")
                        .WithMany("ConsultationSchedules")
                        .HasForeignKey("DiplomaThesisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiplomaThesis");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Department", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Faculty", "Faculty")
                        .WithMany("Departments")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.DiplomaThesis", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Mentor", "Mentor")
                        .WithMany()
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Student", "Student")
                        .WithOne("DiplomaThesis")
                        .HasForeignKey("DiplomaThesisDigitalization.Models.Entities.DiplomaThesis", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mentor");

                    b.Navigation("Student");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Field", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Department", "Department")
                        .WithMany("Fields")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Mentor", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Student", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Field", "Field")
                        .WithMany("Students")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Field");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Title", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Field", "Field")
                        .WithMany("Titles")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("FieldMentor", b =>
                {
                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Field", null)
                        .WithMany()
                        .HasForeignKey("FieldsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiplomaThesisDigitalization.Models.Entities.Mentor", null)
                        .WithMany()
                        .HasForeignKey("MentorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Department", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.DiplomaThesis", b =>
                {
                    b.Navigation("ConsultationSchedules");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Faculty", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Field", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Titles");
                });

            modelBuilder.Entity("DiplomaThesisDigitalization.Models.Entities.Student", b =>
                {
                    b.Navigation("DiplomaThesis")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
