using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Huntress.Characters
{
    public class PatrolBehavior : MonoBehaviour
    {
        Transform playerTransform;
        NavMeshAgent agent;
        [SerializeField] Transform[] patrolPoints;
        CharacterStats character;
        int index = 0;
        public bool isPatrolling;
        public bool isChasing;
        public bool canAttack;

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<CharacterStats>();
        }
        private void Update()
        {

            if (isPatrolling)
            {

                if (CheckIfInPatrolPointRange())
                {
                    if (index < patrolPoints.Length - 1) index++;
                    else index = 0;
                }
                else
                {
                    agent.destination = patrolPoints[index].position;
                }
            }
            if (isChasing && !canAttack)
            {
                agent.destination = playerTransform.position;
            }
            if (canAttack)
            {
                agent.destination = transform.position;
            }
            if (character.health <= 0)
            {
                isChasing = false;
                isPatrolling = false;
                agent.speed = 0;
            }
        }

        bool CheckIfInPatrolPointRange()
        {
            if (Vector3.Distance(patrolPoints[index].position, transform.position) < 0.1f)
            {
                return true;
            }
            else return false;
        }
    }
}
