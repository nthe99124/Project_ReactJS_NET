﻿// <auto-generated />
using System;
using API.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220318033551_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Model.BaseEntity.Bill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BillStatusId")
                        .HasColumnType("int");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("CustomerID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BillStatusId");

                    b.HasIndex("DateOrder");

                    b.HasIndex("UserId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("Model.BaseEntity.BillStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("StatusDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("BillStatus");
                });

            modelBuilder.Entity("Model.BaseEntity.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ImageID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ImageID");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Model.BaseEntity.Cart", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("QuantityPurchased")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Model.BaseEntity.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("Model.BaseEntity.FavoriteList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("CustomerID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteList");
                });

            modelBuilder.Entity("Model.BaseEntity.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<string>("UrlImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Model.BaseEntity.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Model.BaseEntity.NewsImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("ImageID")
                        .HasColumnType("bigint");

                    b.Property<long>("NewsID")
                        .HasColumnType("bigint");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("ImageID");

                    b.HasIndex("NewsID");

                    b.ToTable("NewsImage");
                });

            modelBuilder.Entity("Model.BaseEntity.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandID")
                        .HasColumnType("int");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("PromotionPrice")
                        .HasColumnType("decimal(18,0)");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<decimal>("Warranty")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.HasIndex("BrandID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Model.BaseEntity.ProductColor", b =>
                {
                    b.Property<int>("ColorID")
                        .HasColumnType("int");

                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("ColorID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductColor");
                });

            modelBuilder.Entity("Model.BaseEntity.ProductImage", b =>
                {
                    b.Property<long>("ImageID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductID")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("ImageID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("Model.BaseEntity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Model.BaseEntity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Model.BaseEntity.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CreatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("CreatedBy");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("CreatedOn");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedBy")
                        .HasColumnType("bigint")
                        .HasColumnName("UpdatedBy");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("DateTime")
                        .HasColumnName("UpdatedOn");

                    b.Property<long>("UserID")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Model.BaseEntity.Bill", b =>
                {
                    b.HasOne("Model.BaseEntity.BillStatus", "BillStatus")
                        .WithMany("Bill")
                        .HasForeignKey("BillStatusId");

                    b.HasOne("Model.BaseEntity.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId");

                    b.Navigation("BillStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.BaseEntity.Brand", b =>
                {
                    b.HasOne("Model.BaseEntity.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("Model.BaseEntity.Cart", b =>
                {
                    b.HasOne("Model.BaseEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.BaseEntity.FavoriteList", b =>
                {
                    b.HasOne("Model.BaseEntity.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.BaseEntity.NewsImage", b =>
                {
                    b.HasOne("Model.BaseEntity.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.News", "News")
                        .WithMany("NewsImage")
                        .HasForeignKey("NewsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("News");
                });

            modelBuilder.Entity("Model.BaseEntity.Product", b =>
                {
                    b.HasOne("Model.BaseEntity.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandID");

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Model.BaseEntity.ProductColor", b =>
                {
                    b.HasOne("Model.BaseEntity.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.Product", "Product")
                        .WithMany("ProductColor")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Model.BaseEntity.ProductImage", b =>
                {
                    b.HasOne("Model.BaseEntity.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.Product", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Model.BaseEntity.UserRole", b =>
                {
                    b.HasOne("Model.BaseEntity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.BaseEntity.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Model.BaseEntity.BillStatus", b =>
                {
                    b.Navigation("Bill");
                });

            modelBuilder.Entity("Model.BaseEntity.News", b =>
                {
                    b.Navigation("NewsImage");
                });

            modelBuilder.Entity("Model.BaseEntity.Product", b =>
                {
                    b.Navigation("ProductColor");

                    b.Navigation("ProductImage");
                });

            modelBuilder.Entity("Model.BaseEntity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Model.BaseEntity.User", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Carts");

                    b.Navigation("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
