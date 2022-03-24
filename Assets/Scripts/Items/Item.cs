using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Items
{
    public class Item
    {
        public enum ItemType
        {
            ShortBow,
            MasterShortBow,
            LongBow,
            MasterLongBow,
            PerfectLongBow,
            BearPelt,
            CrocodileLeather,
            BoarPelt,
            WolfPelt,
        }

        public ItemType itemType;
        public int amount;
    }
}
