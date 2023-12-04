using BeamServer.Entities;

namespace BeamServer.Models
{
    public class MarketDto
    {
        public string Price { get; set; }
        public bool MyAsset { get; set; }
        public Monster Monster { get; set; }
        public string MarketplaceId { get; set; }
        public string OrderId { get; set; }
    }

    public class MarketSellDto
    {
        public string Price { get; set; }
        public string TokenId { get; set; }
    }

    public class MarketBuyDto
    {
        public string OrderId { get; set; }
    }
}
