using Microsoft.EntityFrameworkCore;
using SeuPet.Models;
using System;
using System.Collections.Generic;

public class SeuPetContext : DbContext
{
    public DbSet<Pet> Pet { get; set; }
    public DbSet<Adocao> Adocao { get; set; }
    public DbSet<Adotante> Adotante { get; set; }
    //public string DbPath { get; }
    public SeuPetContext(DbContextOptions<SeuPetContext> options) : base(options)
    {
        // var folder = Environment.SpecialFolder.LocalApplicationData;
        // var path = Environment.GetFolderPath(folder);
        // DbPath = System.IO.Path.Join(path, "blogging.db");
    }
   /*  protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(); */
    
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.HasDefaultSchema("SeuPet");
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
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Pet>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Pet>().HasIndex(e => e.Id);


        modelBuilder.Entity<Adocao>()
                    .HasKey(p => p.Id);
        modelBuilder.Entity<Adocao>()
                    .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Adocao>()  
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Adocao>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Adocao>()
                    .Property(e => e.PetId).IsRequired();
        modelBuilder.Entity<Adocao>()
                    .Property(e => e.AdotanteId).IsRequired();
        modelBuilder.Entity<Adocao>().HasIndex(e => e.Id);
        modelBuilder.Entity<Adocao>().HasIndex(e => e.CreateAt);


        modelBuilder.Entity<Adotante>()
                    .HasKey(p => p.Id);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Adotante>()  
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Adotante>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Email).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Sexo).IsRequired();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.DataNascimento).IsRequired();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Nome).IsRequired().HasMaxLength(100);      
        modelBuilder.Entity<Adotante>().HasIndex(e => e.Id);               
    }
}