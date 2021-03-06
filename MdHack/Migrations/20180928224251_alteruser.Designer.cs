﻿// <auto-generated />
using System;
using MdHack.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MdHack.Migrations
{
    [DbContext(typeof(AppDb))]
    [Migration("20180928224251_alteruser")]
    partial class alteruser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MdHack.Model.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Avatar");

                    b.Property<string>("FaceData");

                    b.Property<string>("FaceId");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Passport");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PushToken");

                    b.Property<string>("PushTokenData");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MdHack.Model.ProductStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Data");

                    b.Property<Guid?>("ProductId");

                    b.Property<int?>("Status");

                    b.Property<string>("StatusString");

                    b.Property<Guid?>("User");

                    b.HasKey("Id");

                    b.ToTable("ProductStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
