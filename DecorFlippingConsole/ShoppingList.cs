namespace DecorFlippingConsoleShopping
{
    class FullShoppingList
    {
        public FullShoppingList()
        {
            RealmLists = new List<RealmSpecificShoppingList>();
        }
        public List<RealmSpecificShoppingList> RealmLists {get; set;}
        public long TotalCost()
        {
            long currentCost = 0;
            foreach (var realm in RealmLists)
            {
                currentCost += realm.TotalCost();
            }
            return currentCost;
        }
        public void AddItemToFullList(string name, ShoppingItem item)
        {
            bool hasRealm = false;
            for(int i = 0; i < RealmLists.Count; i++)
            {
                if (RealmLists[i].Name == name)
                {
                    RealmLists[i].AddToList(item);
                    hasRealm = true;
                }
            }
            if (!hasRealm)
            {
                RealmSpecificShoppingList newRealm = new RealmSpecificShoppingList(name);
                newRealm.AddToList(item);
                RealmLists.Add(newRealm);
            }
            
        }
        public override string ToString()
        {
            string output = $"TOTAL COST: {TotalCost()} \n -------------------------------------------------------\n";
            foreach (var realm in RealmLists)
            {
                output += $"{realm.ToString()}-------------------------------- \n";
            }
            return output;
        }
    }

    class RealmSpecificShoppingList
    {
        public RealmSpecificShoppingList(string name)
        {
            Name = name;
            Items = new List<ShoppingItem>();
        }
        public string Name {get; set;}
        public List<ShoppingItem> Items {get; set;}
        public void AddToList(ShoppingItem item)
        {
            Items.Add(item);
        }
        public long TotalCost()
        {
            long cost = 0;
            foreach (var item in Items)
            {
                cost += item.PriceOnRealm;
            }
            return cost;
        }
        public override string ToString()
        {
            string output = $"Realm: {Name}                      Total: {TotalCost()} \n";
            foreach (var item in Items)
            {
                output+= $"{item.ItemName} - ${item.PriceOnRealm} \n";
            }
            return output;
        }
    }

    class ShoppingItem
    {
        public ShoppingItem (string name, string id, long price)
        {
            ItemName = name;
            ItemID = id;
            PriceOnRealm = price;
        }
        public string ItemName {get; set;}
        public string ItemID {get; set;}
        public long PriceOnRealm {get; set;}

        public override string ToString()
        {
            return $"{ItemName} - {ItemID} - {PriceOnRealm}";
        }

    }
}