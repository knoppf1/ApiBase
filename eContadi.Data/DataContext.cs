//using eContadi.Core.Base;
using Pomelo.EntityFrameworkCore.MySql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
//using eContadi.Core.Business;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using eContadi.Core.Business;

namespace eContadi.Data
{
    public class DataContext : DbContext
    {
        private readonly string _connectionString;
        public DataContext(string connectionString)
        {
            _connectionString = connectionString;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_connectionString, mysqlOptions => { mysqlOptions.ServerVersion(new Version(5, 6, 30), ServerType.MySql); });
                //optionsBuilder.UseOracle(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Mapeamento das Tabelas para o Entity
            
            builder.Entity<Categoria>().ToTable("Categoria");
            builder.Entity<Cadastro>().ToTable("Cadastro");
            builder.Entity<Usuario>().ToTable("Usuario");


            ;
            builder.Entity<Categoria>().HasKey(x => x.id);
            builder.Entity<Cadastro>().HasKey(x => x.id);
            builder.Entity<Usuario>().HasKey(x => x.id);


            base.OnModelCreating(builder);
        }
    }
}
