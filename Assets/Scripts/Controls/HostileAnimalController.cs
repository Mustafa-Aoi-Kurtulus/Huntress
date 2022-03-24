using Huntress.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Huntress.Controls
{
    public class HostileAnimalController : MonoBehaviour
    {
        [SerializeField] float walkSpeed;
        [SerializeField] float runSpeed;
        PatrolBehavior patrol;
        AIAnimationController anim;
        CharacterStats character;
        NavMeshAgent agent;
        BoxCollider coll;

        void Awake()
        {
            patrol = GetComponent<PatrolBehavior>();
            anim = GetComponent<AIAnimationController>();
            character = GetComponent<CharacterStats>();
            agent = GetComponent<NavMeshAgent>();
            coll = GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (patrol.isPatrolling)
            {
                agent.speed = walkSpeed;
                anim.SetAnimationStateBool("IsWalking", true);
            }

            if (character.health <= 0)
            {
                anim.SetAnimationStateBool("IsWalking", false);
                anim.SetAnimationStateBool("IsRunning", false);
                anim.SetAnimationStateBool("IsAttacking", false);
                anim.SetAnimationStateBool("IsDead", true);
                coll.enabled = false;
                agent.speed = 0;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player Detection"))
            {
                anim.SetAnimationStateBool("IsWalking", false);
                anim.SetAnimationStateBool("IsRunning", true);
                agent.speed = runSpeed;
                patrol.isPatrolling = false;
                patrol.isChasing = true;
            }

            if (other.gameObject.CompareTag("Player Attack"))
            {
                anim.SetAnimationStateBool("IsRunning", false);
                anim.SetAnimationStateBool("IsAttacking", true);
                patrol.isChasing = false;
                patrol.canAttack = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player Detection"))
            {
                anim.SetAnimationStateBool("IsWalking", true);
                anim.SetAnimationStateBool("IsRunning", false);
                patrol.isPatrolling = true;
                patrol.isChasing = false;
            }

            if (other.gameObject.CompareTag("Player Attack"))
            {
                anim.SetAnimationStateBool("IsRunning", true);
                anim.SetAnimationStateBool("IsAttacking", false);
                patrol.isChasing = true;
                patrol.canAttack = false;
            }
        }
    }
}
