using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Domain.Configurations;

public class HeroiConfiguration : IEntityTypeConfiguration<Heroi>
{
    public void Configure(EntityTypeBuilder<Heroi> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder
            .Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(120);

        builder
            .Property(x => x.NomeHeroi)
            .IsRequired()
            .HasMaxLength(120);

        builder
            .HasIndex(x => x.NomeHeroi)
            .IsUnique();

        builder
            .Property(x => x.DataNascimento)
            .IsRequired();

        builder
            .Property(x => x.Altura)
            .IsRequired();

        builder
            .Property(x => x.Peso)
            .IsRequired();
        
        builder.HasMany(x => x.HeroisSuperPoderes)
            .WithOne(x => x.Heroi)
            .HasForeignKey(x => x.HeroiId);

    }
}