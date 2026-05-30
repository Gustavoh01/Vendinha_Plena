using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Vendinha.Model;

namespace Vendinha.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Divida> Dividas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=vendinha;Username=postgres;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes");

                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.NomeCompleto).HasColumnName("nome_completo");
                entity.Property(c => c.CPF).HasColumnName("cpf");
                entity.Property(c => c.DataNascimento).HasColumnName("data_nascimento");
                entity.Property(c => c.Email).HasColumnName("email");

                entity.HasMany(c => c.Dividas)
                      .WithOne()
                      .HasForeignKey(d => d.ClienteId);
            });

            modelBuilder.Entity<Divida>(entity =>
            {
                entity.ToTable("dividas");

                entity.Property(d => d.Id).HasColumnName("id");
                entity.Property(d => d.Valor).HasColumnName("valor");
                entity.Property(d => d.Status).HasColumnName("status");
                entity.Property(d => d.DataCriacao).HasColumnName("data_criacao");
                entity.Property(d => d.DataPagamento).HasColumnName("data_pagamento");
                entity.Property(d => d.ClienteId).HasColumnName("cliente_id");
            });
        }
    }
}
