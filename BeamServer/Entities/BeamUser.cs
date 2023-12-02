using BeamServer.Models;
using Microsoft.AspNetCore.Identity;

namespace BeamServer.Entities
{
    public class BeamUser : IdentityUser
    {
        public bool RequestStarter { get; set; }
        public string ProfileId { get { return Id; } }
    }
}
