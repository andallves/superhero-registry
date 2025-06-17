using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Domain.Configurations;

public class SuperPoderConfiguration : IEntityTypeConfiguration<SuperPoder>
{
    public void Configure(EntityTypeBuilder<SuperPoder> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Descricao)
            .HasMaxLength(250);

        builder.HasMany(x => x.HeroisSuperPoderes)
            .WithOne(x => x.SuperPoder)
            .HasForeignKey(x => x.SuperPoderId);
    }
}