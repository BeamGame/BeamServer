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
            builder.HasData(new Beamon(1, "Fordin", 45, 49, 49, 65, 65, 45, BeamonType.Grass | BeamonType.Poison));
            builder.HasData(new Beamon(2, "Kroki", 39, 52, 43, 60, 50, 65, BeamonType.Fire));
            builder.HasData(new Beamon(3, "Devidin", 44, 48, 65, 50, 64, 43, BeamonType.Water));
            builder.HasData(new Beamon(4, "Aerodin", 40, 45, 40, 35, 35, 56, BeamonType.Normal | BeamonType.Flying));
            builder.HasData(new Beamon(5, "Weastoat", 30, 56, 35, 25, 35, 72, BeamonType.Normal));
        }
    }
}
