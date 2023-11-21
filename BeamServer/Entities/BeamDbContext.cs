using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeamServer.Entities
{
    public class BeamDbContext : IdentityDbContext<BeamUser>
    {
        public BeamDbContext(DbContextOptions<BeamDbContext> options)
    : base(options)
        {
        }
    }
}
