using Microsoft.EntityFrameworkCore;
using SeuPet.Models;
using System;
using System.Collections.Generic;

public class SeuPetContext : DbContext
{
    public DbSet<Pet> Pet { get; set; }
    public DbSet<Adotante> Adotante { get; set; }
    public SeuPetContext(DbContextOptions<SeuPetContext> options) : base(options){ }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Pet>()
                    .HasKey(p => p.Id);
        modelBuilder.Entity<Pet>()
                    .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Pet>()
                    .Property(e => e.Status).IsRequired();
        modelBuilder.Entity<Pet>()
                    .Property(e => e.Sexo).IsRequired();
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.Foto).HasDefaultValue(string.Empty);
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.Nome).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.Tipo).IsRequired();
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.TipoSanguineo).IsRequired();
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Pet>().HasIndex(e => e.Id);

        modelBuilder.Entity<Adotante>()
                    .HasKey(p => p.Id);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Adotante>()  
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Adotante>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Email).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Sexo).IsRequired();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.DataNascimento).IsRequired();
        modelBuilder.Entity<Adotante>();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Nome).IsRequired().HasMaxLength(100);      
        modelBuilder.Entity<Adotante>()
                    .HasMany(e => e.Pets)
                    .WithOne(e => e.Adotante)
                    .HasForeignKey(e => e.AdotanteId);
        modelBuilder.Entity<Adotante>().HasIndex(e => e.Id);               
    }
}