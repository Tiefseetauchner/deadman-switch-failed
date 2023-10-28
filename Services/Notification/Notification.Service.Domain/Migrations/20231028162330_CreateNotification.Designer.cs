﻿// <auto-generated />
using System;
using DeadmanSwitchFailed.Services.Notification.Service.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeadmanSwitchFailed.Services.Notification.Service.Domain.Migrations
{
    [DbContext(typeof(NotificationContext))]
    [Migration("20231028162330_CreateNotification")]
    partial class CreateNotification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DeadmanSwitchFailed.Services.Notification.Service.Domain.Models.PersistentNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("ContainedData")
                        .HasColumnType("longblob");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("VaultId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
