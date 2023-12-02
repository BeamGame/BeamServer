using System;

namespace BeamServer.Models
{
    [Flags]
    public enum BeamonType
    {
        None = 0,
        Normal = 1,
        Fire = 2,
        Water = 4,
        Grass = 8,
        Electric = 16,
        Ice = 32,
        Fighting = 64,
        Poison = 128,
        Ground = 256,
        Flying = 512,
        Psychic = 1024,
        Bug = 2048,
        Rock = 4096,
        Ghost = 8192,
        Dragon = 16384,
        Dark = 32768,
        Steel = 65536
    }
}
