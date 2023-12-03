using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeamServer.Models;
using Microsoft.Extensions.Hosting;

namespace BeamServer.Entities
{


    public class Beamon
    {
        [Key]
        public int BeamonId { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public BeamonType BeamonType { get; set; }

        public ICollection<Monster> Monsters { get; set; }

        public Beamon()
        {

        }

        public Beamon(int beamonId, string name, int hp, int attack, int defense, int specialAttack, int specialDefense, int speed, BeamonType beamonType)
        {
            BeamonId = beamonId;
            Name = name;
            Hp = hp;
            Attack = attack;
            Defense = defense;
            SpecialAttack = specialAttack;
            SpecialDefense = specialDefense;
            Speed = speed;
            BeamonType = beamonType;
        }
    }
}
