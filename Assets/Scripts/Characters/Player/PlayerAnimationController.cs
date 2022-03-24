using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [Header("Booleans")]
        public bool isEquipAnimationPlaying;
        public bool isDamaged;
        public bool isDead;

        [Header("Game Object References")]
        [SerializeField] GameObject bowInBack;
        [SerializeField] GameObject bowInHand;

        [Header("Component References")]
        [SerializeField] Animator anim;
        [SerializeField] CharacterController controller;

        void Update()
        {
            //Stop the jumping animation when the player character hits the ground
            if (controller.isGrounded) anim.SetBool("Jumping", false);
        }
        public void UpdateVelocity(string velocityName, float amount)
        {
            //Update the velocity variables in the animator
            anim.SetFloat(velocityName, amount);
        }
        public void SetAnimationStateBool(string animationName, bool state)
        {
            anim.SetBool(animationName, state);
        }
        public IEnumerator EquipAnimation()
        {
            isEquipAnimationPlaying = true;
            anim.SetTrigger("Equip");
            anim.SetBool("Armed Up", true);
            bowInBack.SetActive(false);
            yield return new WaitForSeconds(0.9f);
            bowInHand.SetActive(true);
            anim.ResetTrigger("Equip");
            isEquipAnimationPlaying = false;
        }
        public IEnumerator UnequipAnimation()
        {

            isEquipAnimationPlaying = true;
            anim.SetTrigger("Unequip");
            anim.SetBool("Armed Up", false);
            bowInHand.SetActive(false);
            yield return new WaitForSeconds(1.1f);
            bowInBack.SetActive(true);
            anim.ResetTrigger("Unequip");
            isEquipAnimationPlaying = false;
        }
        public IEnumerator ShootArrow()
        {
            //Play the shoot animation once and reset the bow state back to locomotion
            anim.SetBool("HasFired", true);
            yield return new WaitForSeconds(0.7f);
            anim.SetBool("BowReady", false);
            anim.SetBool("DrawArrow", false);
            anim.SetBool("HasFired", false);
        }
    }
}
