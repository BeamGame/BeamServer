using Microsoft.AspNetCore.Identity;

namespace BeamServer.Entities
{
    public class BeamUser : IdentityUser
    {
        public string ProfileId { get { return UserName + Id; } }
    }
}
