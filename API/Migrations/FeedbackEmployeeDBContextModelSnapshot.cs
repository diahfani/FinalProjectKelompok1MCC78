﻿// <auto-generated />
using System;
using API.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ProjectManagementDBContext))]
    partial class FeedbackEmployeeDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_accounts");
                });

            modelBuilder.Entity("API.Model.AccountRole", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<Guid>("AccountGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("account_guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<Guid>("RoleGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_guid");

                    b.HasKey("Guid");

                    b.HasIndex("AccountGuid");

                    b.HasIndex("RoleGuid");

                    b.ToTable("tb_tr_accountrole");
                });

            modelBuilder.Entity("API.Model.Employee", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("fullname");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasColumnName("gender");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("hiring_date");

                    b.Property<Guid?>("ManagerID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("manager_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("NIK")
                        .IsRequired()
                        .HasColumnType("char(6)")
                        .HasColumnName("nik");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone_number");

                    b.HasKey("Guid");

                    b.HasIndex("ManagerID");

                    b.HasIndex("NIK", "Email", "PhoneNumber")
                        .IsUnique();

                    b.ToTable("tb_m_employee");
                });

            modelBuilder.Entity("API.Model.Rating", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<double>("RatingValue")
                        .HasColumnType("float")
                        .HasColumnName("rating_value");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_ratings");
                });

            modelBuilder.Entity("API.Model.Report", b =>
                {
                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(MAX)")
                        .HasColumnName("file_data");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("file_name");

                    b.Property<int>("FileType")
                        .HasColumnType("int")
                        .HasColumnName("file_type");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("subject");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_reports");
                });

            modelBuilder.Entity("API.Model.Role", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Guid");

                    b.ToTable("tb_m_role");

                    b.HasData(
                        new
                        {
                            Guid = new Guid("bad2010a-8d51-4eaf-eccb-08db73d114ff"),
                            CreatedDate = new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7574),
                            ModifiedDate = new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7584),
                            Name = "employee"
                        },
                        new
                        {
                            Guid = new Guid("f0ed952a-0321-4193-3653-08db73d30b74"),
                            CreatedDate = new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7587),
                            ModifiedDate = new DateTime(2023, 6, 24, 19, 23, 10, 927, DateTimeKind.Local).AddTicks(7587),
                            Name = "manager"
                        });
                });

            modelBuilder.Entity("API.Model.Task", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("guid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2")
                        .HasColumnName("deadline");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<Guid>("EmployeeGuid")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_date");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("subject");

                    b.HasKey("Guid");

                    b.HasIndex("EmployeeGuid");

                    b.ToTable("tb_tr_tasks");
                });

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.HasOne("API.Model.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("API.Model.Account", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Model.AccountRole", b =>
                {
                    b.HasOne("API.Model.Account", "Account")
                        .WithMany("AccountRole")
                        .HasForeignKey("AccountGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Model.Role", "Role")
                        .WithMany("AccountRole")
                        .HasForeignKey("RoleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Model.Employee", b =>
                {
                    b.HasOne("API.Model.Employee", "Manager")
                        .WithMany("Subordinates")
                        .HasForeignKey("ManagerID");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("API.Model.Rating", b =>
                {
                    b.HasOne("API.Model.Task", "Task")
                        .WithOne("Rating")
                        .HasForeignKey("API.Model.Rating", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("API.Model.Report", b =>
                {
                    b.HasOne("API.Model.Task", "Task")
                        .WithOne("Report")
                        .HasForeignKey("API.Model.Report", "Guid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("API.Model.Task", b =>
                {
                    b.HasOne("API.Model.Employee", "Employee")
                        .WithMany("Task")
                        .HasForeignKey("EmployeeGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("API.Model.Account", b =>
                {
                    b.Navigation("AccountRole");
                });

            modelBuilder.Entity("API.Model.Employee", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Subordinates");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("API.Model.Role", b =>
                {
                    b.Navigation("AccountRole");
                });

            modelBuilder.Entity("API.Model.Task", b =>
                {
                    b.Navigation("Rating");

                    b.Navigation("Report");
                });
#pragma warning restore 612, 618
        }
    }
}
