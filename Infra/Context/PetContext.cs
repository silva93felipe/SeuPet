using System;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infra.Context
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options)
        {
        }

        public DbSet<Dono> Dono { get; set; }
        //public DbSet<Pet> Pet { get; set; }
        //public DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Dono>()
                        .ToTable("Dono")
                        .HasKey(d => d.Id);

            modelBuilder.Entity<Dono>()
                        .Property(d => d.Ativo).HasDefaultValue("true");  

            modelBuilder.Entity<Dono>()
                        .Property(d => d.CreateAt).HasDefaultValue(DateTime.Now.ToUniversalTime());                    

            modelBuilder.Entity<Dono>()
                        .Property(d => d.UpdateAt).HasDefaultValue(DateTime.Now.ToUniversalTime());

            modelBuilder.Entity<Dono>()                        
                        .Property(d => d.Nome).IsRequired().HasMaxLength(100);
            
            modelBuilder.Entity<Dono>() 
                        .Property(d => d.Cpf).IsRequired().HasMaxLength(11);
            
            modelBuilder.Entity<Dono>() 
                        .Property(d => d.Telefone).IsRequired(false).HasMaxLength(20);

            //modelBuilder.Entity<Pet>().ToTable("Pet");

            //modelBuilder.Entity<Endereco>().ToTable("Endereco");
        }
    }
}