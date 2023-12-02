using BeamServer.Entities;
using BeamServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeamServer.Configuration
{
    public class MoveConfiguration : IEntityTypeConfiguration<Move>
    {
        public void Configure(EntityTypeBuilder<Move> builder)
        {
            builder.HasData(
                new Move
                {
                    MoveId = 1,
                    Name = "Cut"
                },
                new Move
                {
                    MoveId = 2,
                    Name = "Ember"
                }, new Move
                {
                    MoveId = 3,
                    Name = "Growl"
                }, new Move
                {
                    MoveId = 4,
                    Name = "PoisonPowder"
                }, new Move
                {
                    MoveId = 5,
                    Name = "QuickAttack"
                }, new Move
                {
                    MoveId = 6,
                    Name = "SandAttack"
                }, new Move
                {
                    MoveId = 7,
                    Name = "Scratch"
                }, new Move
                {
                    MoveId = 8,
                    Name = "Sing"
                }, new Move
                {
                    MoveId = 9,
                    Name = "SuperSonic"
                }, new Move
                {
                    MoveId = 10,
                    Name = "Surf"
                }, new Move
                {
                    MoveId = 11,
                    Name = "Tackle"
                }, new Move
                {
                    MoveId = 12,
                    Name = "ThunderWave"
                }, new Move
                {
                    MoveId = 13,
                    Name = "Vine"
                });
        }
    }
}
