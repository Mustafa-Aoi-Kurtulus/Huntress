using Huntress.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Huntress.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        Inventory _inventory;
        [SerializeField] List<ItemData> itemData;
        [SerializeField] GameObject descriptionPanel;
        [SerializeField] GameObject gridPrefab;
        [SerializeField] Transform gridHolder;
        public Image activeImage;
        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            RefreshInventoryItems();
        }

        private void RefreshInventoryItems()
        {
            foreach (Item item in _inventory.GetItemList())
            {
                int multiplier = item.amount;

                for (int i = 0; i < multiplier; i++)
                {
                    GameObject grid = Instantiate(gridPrefab, gridHolder);
                    int index;
                    if (item.itemType == Item.ItemType.ShortBow) index = 1;
                    else if (item.itemType == Item.ItemType.MasterShortBow) index = 2;
                    else if (item.itemType == Item.ItemType.LongBow) index = 3;
                    else if (item.itemType == Item.ItemType.MasterLongBow) index = 4;
                    else if (item.itemType == Item.ItemType.PerfectLongBow) index = 5;
                    else if (item.itemType == Item.ItemType.BearPelt) index = 6;
                    else if (item.itemType == Item.ItemType.CrocodileLeather) index = 7;
                    else if (item.itemType == Item.ItemType.BoarPelt) index = 8;
                    else if (item.itemType == Item.ItemType.WolfPelt) index = 9;
                    else index = 0;
                    grid.GetComponent<InventoryGridManager>().data = itemData[index];
                    grid.GetComponent<InventoryGridManager>().descriptionPanel = descriptionPanel;
                }
            }
        }
    }
}
