﻿// <auto-generated />
using ClientsManager.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClientsManager.WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210205133949_InitialMig")]
    partial class InitialMig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ClientsManager.WebApi.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ClientsManager.WebApi.Models.ClienteEndereco", b =>
                {
                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ClienteId", "EnderecoId");

                    b.HasIndex("EnderecoId");

                    b.ToTable("ClientesEnderecos");
                });

            modelBuilder.Entity("ClientsManager.WebApi.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("ClientsManager.WebApi.Models.ClienteEndereco", b =>
                {
                    b.HasOne("ClientsManager.WebApi.Models.Cliente", "Cliente")
                        .WithMany("ClientesEnderecos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientsManager.WebApi.Models.Endereco", "Endereco")
                        .WithMany("ClientesEnderecos")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("ClientsManager.WebApi.Models.Cliente", b =>
                {
                    b.Navigation("ClientesEnderecos");
                });

            modelBuilder.Entity("ClientsManager.WebApi.Models.Endereco", b =>
                {
                    b.Navigation("ClientesEnderecos");
                });
#pragma warning restore 612, 618
        }
    }
}
