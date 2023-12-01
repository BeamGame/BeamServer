using System;
using System.ComponentModel.DataAnnotations;

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
    }
}
