using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters.Player
{
    public class PlayerData
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public int Health { get; set; }
        public string SavedScene { get; set; }

    }
}
