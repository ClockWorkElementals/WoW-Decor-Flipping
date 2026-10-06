namespace DecorFlippingConsoleShopping
{
    class RealmSpecificShoppingList
    {
        public string Name {get; set;}
        public List<ShoppingItem> Items {get; set;}
        public void AddToList(ShoppingItem item)
        {
            Items.Add(item);
        }
        public int TotalCost()
        {
            int cost = 0;
            foreach (var item in Items)
            {
                cost += item.PriceOnRealm;
            }
            return cost;
        }
    }

    class ShoppingItem
    {
        public string ItemName {get; set;}
        public int ItemID {get; set;}
        public int PriceOnRealm {get; set;}

    }
}