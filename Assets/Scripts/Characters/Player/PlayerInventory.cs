using Huntress.UI;
using Huntress.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        public static bool weaponEquipped;

        [SerializeField] UI_Inventory uiInventory;
        CharacterStats character;

        [SerializeField] GameObject bowInBack;
        [SerializeField] GameObject bowInHand;
        [SerializeField] Material shortbowMat;
        [SerializeField] Material masterShortbowMat;
        [SerializeField] Material longbowMat;
        [SerializeField] Material masterLongbowMat;
        [SerializeField] Material perfectLongbowMat;
        Inventory inventory;

        GameObject _currentBow;
        private void Awake()
        {
            inventory = new Inventory();
            uiInventory.SetInventory(inventory);
            character = GetComponent<CharacterStats>();
        }

        public void EquipItem(ItemData item)
        {
            if (item.itemName == "Shortbow")
            {
                SortBow(shortbowMat);
                character.weaponDamage = 3;
            }
            if (item.itemName == "Master Shortbow")
            {
                SortBow(masterShortbowMat);
                character.weaponDamage = 6;
            }
            if (item.itemName == "Longbow")
            {
                SortBow(longbowMat);
                character.weaponDamage = 9;
            }
            if (item.itemName == "Master Longbow")
            {
                SortBow(masterLongbowMat);
                character.weaponDamage = 12;
            }
            if (item.itemName == "Perfect Longbow")
            {
                SortBow(perfectLongbowMat);
                character.weaponDamage = 15;
            }
            weaponEquipped = true;
        }

        private void SortBow(Material material)
        {
            bowInHand.GetComponent<SkinnedMeshRenderer>().material = material;
            bowInBack.GetComponent<MeshRenderer>().material = material;
            bowInBack.SetActive(true);
        }

    }
}
