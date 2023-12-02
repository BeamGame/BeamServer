using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BeamServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BeamServer.Entities
{

    [Index(nameof(MonsterId), nameof(MoveId), IsUnique = true)]
    public class BeamonMove
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BeamonMoveId { get; set; }
        public int MonsterId { get; set; }
        public int MoveId { get; set; }

        public Monster Monster { get; set; }
        public Move Move { get; set; }
    }
}
