## So! Current plans: I want to guarantee I have AT LEAST 1 of every single housing decor item.
  - When one item sells, I want a UI to select what that item is
    - When mail is opened on Aerie Peak, I notice I've sold an Apocothary Table. I search for item and select it.
  - When that item is selected, I want to return what realm it's cheapest on, along with the current price/MV of that item on current realm
    - Apocothary Table is cheapest on Illidan for 920g. It sells on Aerie Peak for 14000g
  - Later, I want the option to see multiple realm data, perhaps in a list.

    
| Item Name        | Purchase From  | Sell On             | Alt 1                   | Alt 2            |
|------------------|----------------|---------------------|-------------------------|------------------|
| Apocathary Table | Illidan (920g) | Aerie Peak (14000g) | Wyrmrest Accord (7365g) | Aggramar (5221g) |
  
## What do I Know:
  - I have the IDs of every furniture item, somewhere
  - I have the names of the realms I intend on looking at

## Steps
  - ~~Make sure the API fuckin works and I can use it lmao~~
    - I've got to add API key to header
  - ~~Make sure I can call the API on specific item IDs in a modular enough way for better expansion~~
  - Parse the return data cleanly to find cheapest realm from the item and to find prices on my realms for said item
    - Last 4 digits are for silver and copper, so find a way to truncate number to just gold
    - Might also help if there's a way to guarantee we find multiples at small price just in case we need multiples of the item
  - Once my outputs are good, work on making inputs better. I don't want to have to search something in the spreadsheet to find my item ids, and I want to query
    multiple things at once.
  - Shopping list on a per-realm basis. If 6 of the things are going to be grabbed off one realm, I want those items and their costs shown together.
    - Buying happens all at once. 3 things to AP, 2 to MG, 1 to WRA should generate a list of 6 to buy
  - UI time? Then determine if this should be an addon. If an addon, maybe that'll be cleaner to generate shopping list and keep track of warehouse. Addon is MUCH later work
  - Eat shit and die, JS. We're going for ANYTHING else. C# is calling my name
