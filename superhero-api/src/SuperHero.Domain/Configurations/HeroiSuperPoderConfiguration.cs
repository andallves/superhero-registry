using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Domain.Configurations;

public class HeroiSuperPoderConfiguration : IEntityTypeConfiguration<HeroiSuperPoder>
{
    public void Configure(EntityTypeBuilder<HeroiSuperPoder> builder)
    {
        builder
            .HasKey(x => new { x.HeroiId, x.SuperPoderId });

        builder
            .HasOne(x => x.Heroi)
            .WithMany(x => x.HeroisSuperPoderes)
            .HasForeignKey(x => x.HeroiId);

        builder
            .HasOne(x => x.SuperPoder)
            .WithMany(x => x.HeroisSuperPoderes)
            .HasForeignKey(x => x.SuperPoderId);
    }
}