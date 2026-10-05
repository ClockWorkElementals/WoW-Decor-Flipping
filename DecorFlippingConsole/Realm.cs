namespace DecorFlippingConsole
{
    public class Realm
    {
        public string product { get; set; }
        public string region { get; set; }
        public string slug { get; set; }
        public string name { get; set; }

    //Methods
    public override string ToString()
    {
        return $"Name: {name} | Region: {region}";
    }

    }
    public class Request
    {
        public string list { get; set; }
    }
    public class Result
    {
        public DateTime lastUpdated { get; set; }
        public List<Realm> realms { get; set; }

        //Methods

        public List<Realm> USRealms()
        {
            List<Realm> US = [];
            foreach (Realm currentRealm in realms)
            {
                if(currentRealm.region == "us") US.Add(currentRealm);
            }
            return US;
        }
    }
    public class Root
    {
        public Request request { get; set; }
        public Result result { get; set; }
    }
}