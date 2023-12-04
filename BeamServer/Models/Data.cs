namespace BeamServer.Models
{
    public class PlayerName
    {
        public string Name { get; set; }
    }

    public class BalanceDto
    {
        public string Address { get; set; }
        public decimal Native { get; set; }
        public decimal BeamonCoin { get; set; }
    }
}
