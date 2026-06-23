using Microsoft.EntityFrameworkCore;
using SeuPet.Domain.Entity;

namespace SeuPet.Domain;
public class SeuPetContext : DbContext
{
    public DbSet<Pet> Pet { get; set; }
    public DbSet<Adotante> Adotante { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Adocao> Adocao { get; set; }
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
                    .Property(e => e.Sexo).IsRequired();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.DataNascimento).IsRequired();
        modelBuilder.Entity<Adotante>()
                    .Property(e => e.Nome).IsRequired().HasMaxLength(100); 
        modelBuilder.Entity<Adotante>().HasIndex(e => e.Id);   

        modelBuilder.Entity<Usuario>()
                    .HasKey(p => p.Id);     
        modelBuilder.Entity<Usuario>()
                    .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Usuario>()  
                    .OwnsOne(e => e.Email, email =>
                    {
                        email.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(150);
                    });
        modelBuilder.Entity<Usuario>()  
                    .Property(e => e.CreateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Usuario>()  
                    .Property(e => e.UpdateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Usuario>().HasIndex(e => e.Id);    
        
        modelBuilder.Entity<Empresa>()
            .HasKey(p => p.Id);     
        modelBuilder.Entity<Empresa>()
            .Property(e => e.Ativo).IsRequired();
        modelBuilder.Entity<Empresa>()
            .Property(e => e.Nome).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Empresa>()  
            .Property(e => e.CreateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Empresa>()  
            .Property(e => e.UpdateAt).HasDefaultValue(DateTime.UtcNow);
        modelBuilder.Entity<Empresa>()
            .OwnsOne(e => e.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradoro).HasColumnName("Logradouro").IsRequired().HasMaxLength(150);
                endereco.Property(e => e.Bairro).HasColumnName("Bairro").IsRequired().HasMaxLength(150);
                endereco.Property(e => e.Cidade).HasColumnName("Cidade").IsRequired().HasMaxLength(150);
                endereco.Property(e => e.Estado).HasColumnName("Estado").IsRequired().HasMaxLength(2);
                endereco.Property(e => e.Numero).HasColumnName("Numero").IsRequired().HasMaxLength(20);

            });
        modelBuilder.Entity<Empresa>().HasIndex(e => e.Id);     
    }
}