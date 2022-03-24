using Cinemachine;
using Huntress.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Huntress.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] float rotationSmoothness;
        [SerializeField] float zoomForce;
        float maxRotationX;
        float minRotationX;
        public Vector3 targetPoint;
        float directionX;
        float directionY;
        float scrollValue;
        float maxScroll;
        Quaternion nextRotation;

        [SerializeField] GameObject shootPoint;
        [SerializeField] GameObject followTarget;
        [SerializeField] GameObject unarmedCamera;
        [SerializeField] GameObject armedCamera;
        [SerializeField] PlayerController pc;
        [SerializeField] Follow followTargetFollow;
        void Update()
        {
            //Get Mouse Input
            Vector2 directionInput = Mouse.current.delta.ReadValue().normalized;
            directionX = directionInput.x;
            directionY = directionInput.y;

            //Get Mouse Scroll Input
            Vector2 scroll = Mouse.current.scroll.ReadValue().normalized;
            scrollValue = scroll.y;

            if (scrollValue > 0 && followTargetFollow.offset.z < maxScroll)
            {
                followTargetFollow.offset += Vector3.forward * zoomForce;
            }
            if (scrollValue < 0 && followTargetFollow.offset.z > -maxScroll)
            {
                followTargetFollow.offset += Vector3.back * zoomForce;
            }

            if (Mouse.current.rightButton.isPressed && !pc.isAttackReady)
            {
                followTarget.transform.rotation *= Quaternion.AngleAxis(directionX * speed * 2, Vector3.up);
                followTarget.transform.rotation *= Quaternion.AngleAxis(directionY * speed, Vector3.right);
            }
            if (Mouse.current.leftButton.isPressed && pc.isAttackReady)
            {
                followTarget.transform.rotation *= Quaternion.AngleAxis(directionX * speed * 2, Vector3.up);
                followTarget.transform.rotation *= Quaternion.AngleAxis(directionY * speed, Vector3.left);
            }


            nextRotation = Quaternion.Lerp(followTarget.transform.rotation, nextRotation, Time.deltaTime * rotationSmoothness);

            var angles = followTarget.transform.localEulerAngles;
            angles.z = 0;

            var angleX = followTarget.transform.localEulerAngles.x;

            if (pc.isBowReady)
            {
                unarmedCamera.SetActive(false);
                armedCamera.SetActive(true);
                maxRotationX = 345f;
                minRotationX = 35f;
                maxScroll = 1;
            }
            else if (!pc.isBowReady && !pc.isAttackReady)
            {
                unarmedCamera.SetActive(true);
                armedCamera.SetActive(false);
                maxRotationX = 359f;
                minRotationX = 35f;
                maxScroll = 1.5f;
            }

            //Clamp Up/Down rotation
            if (angleX > 180 && angleX < maxRotationX)
            {
                angles.x = maxRotationX;
            }
            else if (angleX < 180 && angleX > minRotationX)
            {
                angles.x = minRotationX;
            }

            followTarget.transform.localEulerAngles = angles;

            //Set rotation for the arrow
            RaycastHit hit;
            Ray ray = new Ray(shootPoint.transform.position, shootPoint.transform.forward);
            if (Physics.Raycast(ray, out hit)) targetPoint = hit.point;
        }
    }
}