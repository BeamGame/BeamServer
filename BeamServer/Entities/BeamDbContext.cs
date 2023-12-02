using BeamServer.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeamServer.Entities
{
    public class BeamDbContext : IdentityDbContext<BeamUser>
    {
        public DbSet<Beamon> Beamons { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<BeamonMove> BeamonMoves { get; set; }

        public BeamDbContext(DbContextOptions<BeamDbContext> options)
    : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BeamonConfiguration());
            modelBuilder.ApplyConfiguration(new MoveConfiguration());
        }
    }
}
