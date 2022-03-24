using Huntress.Controls;
using Huntress.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Huntress.Characters.Player
{
    public class PlayerInputActions : MonoBehaviour
    {
        public bool interactionButtonPressed;

        [Header("Game Object References")]
        [SerializeField] Transform followTarget;
        [SerializeField] Transform shootPoint;
        [SerializeField] Transform parentTarget;
        [Header("Script References")]
        PlayerController pc;
        [SerializeField] PlayerAnimationController pcAnim;
        ObjectPool pool;
        private void Awake()
        {
            pc = GetComponent<PlayerController>();
            pool = GetComponent<ObjectPool>();
        }

        public void OnMovement(InputAction.CallbackContext value)
        {
            //Save player input as Vector2
            Vector2 movementInput = value.ReadValue<Vector2>();
            //Use the player input to change direction variables
            pc.directionX = movementInput.x;
            pc.directionZ = movementInput.y;
            pc.desiredRotation = followTarget.transform.localRotation.eulerAngles.y;
            transform.rotation = Quaternion.AngleAxis(pc.desiredRotation, Vector3.up);
        }

        public void OnRun(InputAction.CallbackContext value)
        {
            if (value.started) pc.isRunPressed = true;
            if (value.performed) pc.isRunPressed = false;
        }
        public void OnJump(InputAction.CallbackContext value)
        {
            if (value.performed)
            {
                pc.isJumpPressed = true;
                pcAnim.SetAnimationStateBool("Jumping", true);
            }
            if (value.canceled) pc.isJumpPressed = false;
        }

        public void OnUnsheathe(InputAction.CallbackContext value)
        {
            if (PlayerInventory.weaponEquipped)
            {
                if (!pcAnim.isEquipAnimationPlaying)
                {
                    pc.isBowReady = true;
                    StartCoroutine(pcAnim.EquipAnimation());
                }
            }
        }

        public void OnSheathe(InputAction.CallbackContext value)
        {
            if (!pcAnim.isEquipAnimationPlaying)
            {
                pc.isBowReady = false;
                StartCoroutine(pcAnim.UnequipAnimation());
            }
        }
        public void OnAttack(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //Start counting attack time as soon as the player presses the attack button and increase it every second, the player is holding it down (max 5)
                pc.isAttackReady = true;
                pcAnim.SetAnimationStateBool("DrawArrow", true);
                pcAnim.SetAnimationStateBool("BowReady", true);
            }
            if (value.performed)
            {
                pc.isAttackReady = false;
                if (pc.attackTime < 1)
                {
                    //If the player lets go of the attack button in less than a second attack is cancelled

                    pcAnim.SetAnimationStateBool("DrawArrow", false);
                }
                if (pc.attackTime >= 1)
                {
                    //If the player lets go of the attack button after holding it down for at least a second attack action happens
                    StartCoroutine(pcAnim.ShootArrow());
                    GameObject arrow = pool.PoolObject();
                    arrow.transform.position = shootPoint.position;
                    arrow.transform.rotation = shootPoint.rotation;
                    arrow.transform.parent = parentTarget.parent;
                }
                //Save attack time variable and reset it
                PlayerController.savedAttackTime = Mathf.FloorToInt(pc.attackTime);
                pc.attackTime = 0;
            }

        }
        public void OnInteraction(InputAction.CallbackContext value)
        {
            if (FindObjectOfType<SceneLoader>().canChangeScenes) interactionButtonPressed = true;
        }

        public void OnSave(InputAction.CallbackContext value)
        {
            PlayerPersistance.SaveData(GetComponent<PlayerDataHandler>());
        }

        public void OnLoad(InputAction.CallbackContext value)
        {
            GetComponent<PlayerDataHandler>().LoadGame();
        }
    }
}
