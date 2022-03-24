using Huntress.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Status Variables")]
        public int health;
        public int strength;
        public int weaponDamage;

        [Header("Script References")]
        CharacterStats pc;

        private void Start()
        {
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        }
        public void TakeDamage(int damage, int attackTime, int strenghtMultiplier)
        {
            int calculatedDamage = damage * strenghtMultiplier * attackTime;
            health -= calculatedDamage;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Arrow"))
            {
                TakeDamage(pc.weaponDamage, PlayerController.savedAttackTime, pc.strength);
            }
        }
    }
}
