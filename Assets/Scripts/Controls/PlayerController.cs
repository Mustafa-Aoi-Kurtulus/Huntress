using Huntress.Characters.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Huntress.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement and Rotation")]
        public float moveSpeed;
        [SerializeField] float walkMoveSpeed;
        [SerializeField] float runMoveSpeed;
        public float directionX;
        public float directionZ;
        public float rotationY;
        public float desiredRotation;
        [SerializeField] float gravity = -9.81f;
        float jumpDirection;
        [SerializeField] float jumpForce;
        public bool isRunPressed;
        public bool isJumpPressed;

        [Header("Attack")]
        public float attackTime = 0;
        public static int savedAttackTime;
        public bool isBowReady;
        public bool isAttackReady;
        [SerializeField] GameObject crosshair;

        [Header("Component References")]
        [SerializeField] CharacterController controller;
        [SerializeField] PlayerInput playerInput;
        [SerializeField] PlayerAnimationController pcAnim;

        [Header("Game Object References")]
        [SerializeField] GameObject followCameraTarget;

        void Update()
        {
            //Get x and z axis values from the OnMove function and create a direction variable
            Vector3 direction = new Vector3(directionX, 0, directionZ);

            //Update the jumpDirection variable with jumpForce and gravity
            if (isJumpPressed && controller.isGrounded && !isAttackReady) jumpDirection = jumpForce;
            jumpDirection -= gravity * Time.deltaTime;

            //Update the y axis of the direction variable to be be equal to jumpDirection
            direction.y = jumpDirection;

            //Update the movementSpeed based on the sprint key
            moveSpeed = isRunPressed ? runMoveSpeed : walkMoveSpeed;

            //Move the player character based on the direction variable
            direction = transform.TransformDirection(direction);
            controller.Move(direction * Time.deltaTime * moveSpeed);

            rotationY = followCameraTarget.transform.eulerAngles.y;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationY, transform.eulerAngles.z);


            HandleLocomotionAnimations();

            //Switch Action Maps
            if (!isBowReady) playerInput.SwitchCurrentActionMap("Player Actions");
            if (isBowReady) playerInput.SwitchCurrentActionMap("Armed Player Actions");

            if (isBowReady) crosshair.SetActive(true);
            else crosshair.SetActive(false);

            if (attackTime < 5 && isAttackReady) attackTime += Time.deltaTime;
        }
        void HandleLocomotionAnimations()
        {
            //Update animations' speed when the player holds down the sprint key, round up the direction values when the player wants to move diagonally
            if (directionX < 1 && directionX > 0) directionX = 1;
            if (directionX > -1 && directionX < 0) directionX = -1;
            if (directionZ < 1 && directionZ > 0) directionZ = 1;
            if (directionZ > -1 && directionZ < 0) directionZ = -1;
            pcAnim.UpdateVelocity("Velocity X", moveSpeed * directionX);
            pcAnim.UpdateVelocity("Velocity Z", moveSpeed * directionZ);
        }
    }
}