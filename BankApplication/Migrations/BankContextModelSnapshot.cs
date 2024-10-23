﻿// <auto-generated />
using System;
using BankApplication.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankApplication.Migrations
{
    [DbContext(typeof(BankContext))]
    partial class BankContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankApplication.Client.ClientBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("BankApplication.Client.ClientCreditCard", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("BalanceId")
                        .HasColumnType("int");

                    b.Property<int>("CVVcode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3)
                        .HasColumnType("int")
                        .HasDefaultValue(555);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasDefaultValue("6666666666666666");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Fortune")
                        .HasColumnType("float");

                    b.Property<int>("PIN")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(4)
                        .HasColumnType("int")
                        .HasDefaultValue(1337);

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("BankApplication.Client.ClientData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BalanceId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValue("Popovich");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("BalanceId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BankApplication.Client.CreditData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BalanceID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreditEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreditStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("ACTIVE");

                    b.Property<double>("CreditSum")
                        .HasMaxLength(10)
                        .HasColumnType("float");

                    b.Property<string>("CreditType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(255)")
                        .HasDefaultValue("REVOLVING_CREDIT");

                    b.HasKey("Id");

                    b.HasIndex("BalanceID");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("BankApplication.Client.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientCreditCardId")
                        .HasColumnType("int");

                    b.Property<int>("CreditCardID")
                        .HasColumnType("int");

                    b.Property<string>("Payee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<double>("TransactionAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientCreditCardId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankApplication.Client.ClientCreditCard", b =>
                {
                    b.HasOne("BankApplication.Client.ClientBalance", "Balance")
                        .WithMany("CreditCardsForDB")
                        .HasForeignKey("BalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("BankApplication.Client.ClientData", b =>
                {
                    b.HasOne("BankApplication.Client.ClientBalance", "Balance")
                        .WithMany()
                        .HasForeignKey("BalanceId");

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("BankApplication.Client.CreditData", b =>
                {
                    b.HasOne("BankApplication.Client.ClientBalance", "Balance")
                        .WithMany("CreditsForDB")
                        .HasForeignKey("BalanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Balance");
                });

            modelBuilder.Entity("BankApplication.Client.Transaction", b =>
                {
                    b.HasOne("BankApplication.Client.ClientCreditCard", "ClientCreditCard")
                        .WithMany("TransactionsForDB")
                        .HasForeignKey("ClientCreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientCreditCard");
                });

            modelBuilder.Entity("BankApplication.Client.ClientBalance", b =>
                {
                    b.Navigation("CreditCardsForDB");

                    b.Navigation("CreditsForDB");
                });

            modelBuilder.Entity("BankApplication.Client.ClientCreditCard", b =>
                {
                    b.Navigation("TransactionsForDB");
                });
#pragma warning restore 612, 618
        }
    }
}
