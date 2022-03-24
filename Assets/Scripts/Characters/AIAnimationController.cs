using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters
{
    public class AIAnimationController : MonoBehaviour
    {
        [SerializeField] Animator anim;
        public void SetAnimationStateBool(string animationName, bool state)
        {
            anim.SetBool(animationName, state);
        }
    }
}