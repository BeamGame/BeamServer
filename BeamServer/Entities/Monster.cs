using BeamServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeamServer.Entities
{
    public class Monster
    {
        [Key]
        public int MonsterId { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }

        public int BeamonId { get; set; }
        public Beamon Beamon { get; set; }

        public ICollection<BeamonMove> BeamonMoves { get; set; }
    }
}
