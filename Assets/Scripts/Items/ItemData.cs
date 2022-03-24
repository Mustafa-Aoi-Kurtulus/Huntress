using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Items
{
    [CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObjects/Item")]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int value;
        public string description;
        public Sprite icon;
    }
}
