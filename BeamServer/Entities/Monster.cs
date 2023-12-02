using BeamServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeamServer.Entities
{
    public class Monster
    {
        [Key]
        public int MonsterId { get; set; }
        public int BeamonId { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
        public int HpIV { get; set; }
        public int AttackIV { get; set; }
        public int DefenseIV { get; set; }
        public int SpecialAttackIV { get; set; }
        public int SpecialDefenseIV { get; set; }
        public int SpeedIV { get; set; }
        public int HpEV { get; set; }
        public int AttackEV { get; set; }
        public int DefenseEV { get; set; }
        public int SpecialAttackEV { get; set; }
        public int SpecialDefenseEV { get; set; }
        public int SpeedEV { get; set; }
        public BeamonNature Nature { get; set; }

        public Beamon Beamon { get;}
    }
}
