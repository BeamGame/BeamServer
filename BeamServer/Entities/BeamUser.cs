using BeamServer.Models;
using Microsoft.AspNetCore.Identity;

namespace BeamServer.Entities
{
    public class BeamUser : IdentityUser
    {
        public string ProfileId { get { return Constants.ServerId + UserName + Id; } }
    }
}
