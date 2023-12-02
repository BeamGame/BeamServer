namespace BeamServer.Models
{
    public class Profile
    {
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string externalId { get; set; }
        public string externalEntityId { get; set; }
        public string gameId { get; set; }
        public object userId { get; set; }
        public object userConnectionCreatedAt { get; set; }
        public List<Wallet> wallets { get; set; }
    }


    public class Wallet
    {
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string externalId { get; set; }
        public string address { get; set; }
        public int chainId { get; set; }
        public bool custodial { get; set; }
        public string profileId { get; set; }
    }
}
