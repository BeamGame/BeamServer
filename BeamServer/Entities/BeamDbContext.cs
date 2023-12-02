using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeamServer.Entities
{
    public class BeamDbContext : IdentityDbContext<BeamUser>
    {
        public DbSet<Beamon> Beamons { get; set; }
        public DbSet<Monster> Monsters { get; set; }

        public BeamDbContext(DbContextOptions<BeamDbContext> options)
    : base(options)
        {
        }
    }
}
