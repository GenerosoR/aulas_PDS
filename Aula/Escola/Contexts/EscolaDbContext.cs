using System;
using System.Collections.Generic;
using System.Text;
using Escola.Models;
using Microsoft.EntityFrameworkCore;

namespace Escola.Contexts
{
    internal class EscolaDbContext : DbContext
    {
        private string connectionString;
        public EscolaDbContext(string connectionString = null)
        {
            this.connectionString = connectionString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityAluno = modelBuilder.Entity<Aluno>();

            entityAluno.ToTable("alunos");

            entityAluno.Property(c => c.Codigo).HasColumnName("codigo");
            entityAluno.Property(c => c.Nome).HasColumnName("nome");
            entityAluno.Property(c => c.Email).HasColumnName("email");
            entityAluno.Property(c => c.DataNascimento).HasColumnName("datanascimento");
            entityAluno.HasKey(c => c.Codigo);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(connectionString)) 
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
