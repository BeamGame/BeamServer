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
        public int Exp { get; set; }
      
        public Beamon Beamon { get;}
    }
}
