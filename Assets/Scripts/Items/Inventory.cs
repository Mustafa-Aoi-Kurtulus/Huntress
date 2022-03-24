using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Huntress.Items
{
    public class Inventory
    {
        private List<Item> itemList;

        public Inventory()
        {
            itemList = new List<Item>();

            AddItem(new Item { itemType = Item.ItemType.ShortBow, amount = 1});
            AddItem(new Item { itemType = Item.ItemType.MasterShortBow, amount = 1});
            AddItem(new Item { itemType = Item.ItemType.LongBow, amount = 1});
            AddItem(new Item { itemType = Item.ItemType.MasterLongBow, amount = 1});
            AddItem(new Item { itemType = Item.ItemType.PerfectLongBow, amount = 1});
            AddItem(new Item { itemType = Item.ItemType.BearPelt, amount = 2 });
            AddItem(new Item { itemType = Item.ItemType.CrocodileLeather, amount = 2 });
            AddItem(new Item { itemType = Item.ItemType.BoarPelt, amount = 2 });
            AddItem(new Item { itemType = Item.ItemType.WolfPelt, amount = 2 });
        }

        public void AddItem(Item item)
        {
            itemList.Add(item);
        }

        public void RemoveItem(Item item)
        {
            itemList.Remove(item);
        }
        public List<Item> GetItemList()
        {
            return itemList;
        }
    }
}
