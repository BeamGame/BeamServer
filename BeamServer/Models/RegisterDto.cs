namespace BeamServer.Models
{
    public class RegisterDto
    {
        /// <summary>
        /// The user's name 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// <summary>
        /// The user's email address 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
