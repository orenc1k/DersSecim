﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ODTUDersSecim.Models;

#nullable disable

namespace ODTUDersSecim.Migrations
{
    [DbContext(typeof(ODTUDersSecimDBContext))]
    partial class ODTUDersSecimDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ODTUDersSecim.Models.AvailableCourses", b =>
                {
                    b.Property<int>("AvailableCoursesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AvailableCoursesId"));

                    b.Property<int?>("SubjectCode")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectsSubjectCode")
                        .HasColumnType("integer");

                    b.HasKey("AvailableCoursesId");

                    b.HasIndex("SubjectsSubjectCode");

                    b.ToTable("AvailableCourses");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.Departments", b =>
                {
                    b.Property<int>("DeptCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DeptCode"));

                    b.Property<string>("DeptFullName")
                        .HasColumnType("text");

                    b.Property<string>("DeptShortName")
                        .HasColumnType("text");

                    b.HasKey("DeptCode");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.ElectiveCourses", b =>
                {
                    b.Property<int>("ElectiveCoursesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ElectiveCoursesId"));

                    b.Property<int?>("AvailableCoursesId")
                        .HasColumnType("integer");

                    b.Property<int?>("DepartmentsDeptCode")
                        .HasColumnType("integer");

                    b.Property<int?>("DeptCode")
                        .HasColumnType("integer");

                    b.Property<int?>("ElectiveType")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectCode")
                        .HasColumnType("integer");

                    b.HasKey("ElectiveCoursesId");

                    b.HasIndex("AvailableCoursesId");

                    b.HasIndex("DepartmentsDeptCode");

                    b.ToTable("ElectiveCourses");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.MustCourses", b =>
                {
                    b.Property<int>("MustCourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MustCourseId"));

                    b.Property<int?>("DepartmentsDeptCode")
                        .HasColumnType("integer");

                    b.Property<int?>("DeptCode")
                        .HasColumnType("integer");

                    b.Property<int>("Semester")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectCode")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectsSubjectCode")
                        .HasColumnType("integer");

                    b.HasKey("MustCourseId");

                    b.HasIndex("DepartmentsDeptCode");

                    b.HasIndex("SubjectsSubjectCode");

                    b.ToTable("MustCourses");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.SectionDays", b =>
                {
                    b.Property<int>("SectionDaysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SectionDaysId"));

                    b.Property<string>("Day1")
                        .HasColumnType("text");

                    b.Property<string>("Day2")
                        .HasColumnType("text");

                    b.Property<string>("Day3")
                        .HasColumnType("text");

                    b.Property<string>("InstructorName")
                        .HasColumnType("text");

                    b.Property<string>("Place")
                        .HasColumnType("text");

                    b.Property<int?>("SectionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectCode")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectSectionsSectionId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectsSubjectCode")
                        .HasColumnType("integer");

                    b.Property<string>("Time1")
                        .HasColumnType("text");

                    b.Property<string>("Time2")
                        .HasColumnType("text");

                    b.Property<string>("Time3")
                        .HasColumnType("text");

                    b.HasKey("SectionDaysId");

                    b.HasIndex("SubjectSectionsSectionId");

                    b.HasIndex("SubjectsSubjectCode");

                    b.ToTable("SectionDays");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.SubjectSections", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SectionId"));

                    b.Property<string>("EndChar")
                        .HasColumnType("text");

                    b.Property<string>("EndGrade")
                        .HasColumnType("text");

                    b.Property<string>("GivenDept")
                        .HasColumnType("text");

                    b.Property<float?>("MaxCumGpa")
                        .HasColumnType("real");

                    b.Property<int?>("MaxYear")
                        .HasColumnType("integer");

                    b.Property<float?>("MinCumGpa")
                        .HasColumnType("real");

                    b.Property<int?>("MinYear")
                        .HasColumnType("integer");

                    b.Property<int>("SectionCode")
                        .HasColumnType("integer");

                    b.Property<string>("StartChar")
                        .HasColumnType("text");

                    b.Property<string>("StartGrade")
                        .HasColumnType("text");

                    b.Property<int?>("SubjectCode")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectsSubjectCode")
                        .HasColumnType("integer");

                    b.HasKey("SectionId");

                    b.HasIndex("SubjectsSubjectCode");

                    b.ToTable("SubjectSections");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.Subjects", b =>
                {
                    b.Property<int>("SubjectCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SubjectCode"));

                    b.Property<int?>("DepartmentsDeptCode")
                        .HasColumnType("integer");

                    b.Property<int?>("DeptCode")
                        .HasColumnType("integer");

                    b.Property<float?>("EctsCredit")
                        .HasColumnType("real");

                    b.Property<float?>("SubjectCredit")
                        .HasColumnType("real");

                    b.Property<string>("SubjectLevel")
                        .HasColumnType("text");

                    b.Property<string>("SubjectName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SubjectType")
                        .HasColumnType("text");

                    b.HasKey("SubjectCode");

                    b.HasIndex("DepartmentsDeptCode");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.AvailableCourses", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.Subjects", "Subjects")
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectCode");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.ElectiveCourses", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.AvailableCourses", "AvailableCourses")
                        .WithMany()
                        .HasForeignKey("AvailableCoursesId");

                    b.HasOne("ODTUDersSecim.Models.Departments", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptCode");

                    b.Navigation("AvailableCourses");

                    b.Navigation("Departments");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.MustCourses", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.Departments", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptCode");

                    b.HasOne("ODTUDersSecim.Models.Subjects", "Subjects")
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectCode");

                    b.Navigation("Departments");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.SectionDays", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.SubjectSections", "SubjectSections")
                        .WithMany()
                        .HasForeignKey("SubjectSectionsSectionId");

                    b.HasOne("ODTUDersSecim.Models.Subjects", "Subjects")
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectCode");

                    b.Navigation("SubjectSections");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.SubjectSections", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.Subjects", "Subjects")
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectCode");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("ODTUDersSecim.Models.Subjects", b =>
                {
                    b.HasOne("ODTUDersSecim.Models.Departments", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentsDeptCode");

                    b.Navigation("Departments");
                });
#pragma warning restore 612, 618
        }
    }
}
