using BeamServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeamServer.Entities
{
    public class Move
    {
        [Key]
        public int MoveId { get; set; }
        public string Name { get; set; }
    }
}
