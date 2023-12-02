using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeamServer.Configuration
{
    public class BeamonConfiguration : IEntityTypeConfiguration<Beamon>
    {
        public void Configure(EntityTypeBuilder<Beamon> builder)
        {
            builder.HasData(new Beamon(1, "Bulbastache", 45, 49, 49, 65, 65, 45, BeamonType.Grass | BeamonType.Poison));
            builder.HasData(new Beamon(4, "Charmustache", 39, 52, 43, 60, 50, 65, BeamonType.Fire));
            builder.HasData(new Beamon(7, "Squirtache", 44, 48, 65, 50, 64, 43, BeamonType.Water));
            builder.HasData(new Beamon(16, "Pidgstache", 40, 45, 40, 35, 35, 56, BeamonType.Normal | BeamonType.Flying));
            builder.HasData(new Beamon(19, "Rattatache", 30, 56, 35, 25, 35, 72, BeamonType.Normal));
        }
    }
}
