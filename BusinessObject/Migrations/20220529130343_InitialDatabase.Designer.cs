﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessObject.Migrations
{
    [DbContext(typeof(eBookStoreDbContext))]
    [Migration("20220529130343_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusinessObject.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("state");

                    b.Property<string>("Zip")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("zip");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BusinessObject.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Advance")
                        .HasColumnType("money")
                        .HasColumnName("advance");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("notes");

                    b.Property<decimal>("Price")
                        .HasColumnType("money")
                        .HasColumnName("price");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("published_date");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int")
                        .HasColumnName("publisher_id");

                    b.Property<decimal>("Royalty")
                        .HasColumnType("money")
                        .HasColumnName("royalty");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("type");

                    b.Property<decimal>("YearToDateSales")
                        .HasColumnType("money")
                        .HasColumnName("ytd_sales");

                    b.HasKey("BookId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BusinessObject.BookAuthor", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    b.Property<int>("AuthorOrder")
                        .HasColumnType("int")
                        .HasColumnName("author_order");

                    b.Property<decimal>("RoyaltyPercentage")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("royalty_percentage");

                    b.HasKey("AuthorId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("BusinessObject.Publisher", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("publisher_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("country");

                    b.Property<string>("PublisherName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("publisher_name");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("state");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleDesc")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("role_desc");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BusinessObject.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("first_name");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("hire_date");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("middle_name");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("password");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int")
                        .HasColumnName("publisher_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("source");

                    b.HasKey("UserId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessObject.Book", b =>
                {
                    b.HasOne("BusinessObject.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("BusinessObject.BookAuthor", b =>
                {
                    b.HasOne("BusinessObject.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BusinessObject.User", b =>
                {
                    b.HasOne("BusinessObject.Publisher", "Publisher")
                        .WithMany("Users")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publisher");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObject.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BusinessObject.Book", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("BusinessObject.Publisher", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
