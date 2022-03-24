using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters.Player
{
    public class SetAim : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float targetRotationX;
        private void Update()
        {
            targetRotationX = target.localEulerAngles.x;
            transform.eulerAngles = new Vector3(targetRotationX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}