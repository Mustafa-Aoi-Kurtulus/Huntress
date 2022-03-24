using Huntress.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Huntress.Controls
{
    public class GuardController : MonoBehaviour
    {
        [SerializeField] float walkSpeed;
        [SerializeField] float runSpeed;

        [SerializeField] PatrolBehavior patrol;
        [SerializeField] AIAnimationController anim;

        [SerializeField] NavMeshAgent agent;
        void Update()
        {
            if (patrol.isPatrolling)
            {
                agent.speed = walkSpeed;
                anim.SetAnimationStateBool("IsWalking", true);
            }
            if (patrol.isChasing)
            {
                agent.speed = runSpeed;
                anim.SetAnimationStateBool("IsRunning", true);
            }
            if (patrol.canAttack)
            {
                anim.SetAnimationStateBool("IsAttacking", true);
            }
        }
    }
}
