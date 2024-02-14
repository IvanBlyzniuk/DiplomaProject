using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Enemy
{
    [RequireComponent(typeof(SteeringManager))]
    public class EnemyController : MonoBehaviour, IResettable
    {
        private bool isActive = false;
        private SteeringManager steeringManager;

        private Vector3 initialPosition;

        private int currentSeekTargetIndex = 0;
        private int seekTargetIncrementer = 1;

        [SerializeField] private EnemyParamsSO enemyParams;
        [SerializeField] private List<GameObject> patrollingPath;
        [SerializeField] private bool circlePatrolling;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            steeringManager.Init(enemyParams.maxVelocity, enemyParams.maxForce);
            initialPosition = transform.position;
        }

        void Update()
        {
            if (!isActive)
                return;
            steeringManager.AvoidCollisions(enemyParams.seeAheadDistance, enemyParams.maxAvoidForce);
            var currentSeekTarget = patrollingPath[currentSeekTargetIndex];
            steeringManager.Seek(currentSeekTarget);
            if (Vector2.Distance(transform.position, currentSeekTarget.transform.position) < enemyParams.seekTargetReachedDistance)
            {
                currentSeekTargetIndex += seekTargetIncrementer;
                if (currentSeekTargetIndex == patrollingPath.Count)
                {
                    if(circlePatrolling)
                    {
                        currentSeekTargetIndex = 0;
                    }
                    else
                    {
                        currentSeekTargetIndex--;
                        seekTargetIncrementer = -1;
                    }
                }
                if(currentSeekTargetIndex == -1)
                {
                    currentSeekTargetIndex++;
                    seekTargetIncrementer = 1;
                }
            }
            
        }

        public void Activate()
        {
            isActive = true;
        }

        public void ResetState()
        {
            transform.position = initialPosition;
            steeringManager.ResetStearing();
            currentSeekTargetIndex = 0;
            isActive = false;
        }
    }
}
