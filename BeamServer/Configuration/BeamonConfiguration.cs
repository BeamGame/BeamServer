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
            builder.HasData(new Beamon(1, "Fordin", 45, 40, 40, 65, 65, 45, BeamonType.Grass | BeamonType.Poison));
            builder.HasData(new Beamon(2, "Kroki", 44, 48, 65, 50, 64, 43, BeamonType.Water));
            builder.HasData(new Beamon(3, "Devidin", 40, 52, 43, 60, 50, 60, BeamonType.Fire));
            builder.HasData(new Beamon(4, "Aerodin", 60, 45, 50, 80, 80, 70, BeamonType.Flying | BeamonType.Poison));
            builder.HasData(new Beamon(5, "Weastoat", 35, 60, 44, 40, 54, 55, BeamonType.Poison));
        }
    }
}
