using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities.Hero;

namespace SuperHero.Infra.Context;

public class SuperHeroDbContext : BaseDbContext
{
    public DbSet<Heroi> Herois { get; set; }
    public DbSet<SuperPoder> SuperPoderes { get; set; }
    public DbSet<HeroiSuperPoder> HeroisSuperPoderes { get; set; } 

    public SuperHeroDbContext(DbContextOptions<SuperHeroDbContext> options) : base(options)
    {
        Schema = "SuperHerois";
        Assembly = GetType().Assembly;
    }
    
}