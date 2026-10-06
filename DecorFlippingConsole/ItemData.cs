namespace DecorFlippingConsoleItems
{
        public class Request
    {
        public string region { get; set; }
        public int itemId { get; set; }
        public string period { get; set; }
    }

    public class Result
    {
        //public DateTime lastUpdated { get; set; }
        //public DateTime lastSeen { get; set; }
        public long price { get; set;  }
        public long quantity { get; set; }
        public List<string> realms { get; set; }

        public override string ToString()
        {
            return $"Realm: {realms[0]} | Price: {price/10000}g | Quantity: {quantity}";
        }
    }

    public class Root
    {
        public Request request { get; set; }
        public List<Result> result { get; set; }
        //Should produce the list in ascending order of price
        public List<Result> orderbyPrice()
        {
            return result.OrderBy(r=>r.price).ToList();
        }
    }
}