using App.World.Entity.Minion;
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
        private Rigidbody2D rigidBody;

        private Vector3 initialPosition;

        private int currentSeekTargetIndex = 0;
        private int seekTargetIncrementer = 1;

        private ContactFilter2D contactFilter;
        private Rigidbody2D agrroedTarget;
        private Collider2D[] detectedUnits;

        [SerializeField] private EnemyParamsSO enemyParams;
        [SerializeField] private Collider2D detectionCollider;
        [SerializeField] private bool circlePatrolling;
        [SerializeField] private List<GameObject> patrollingPath;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            rigidBody = GetComponent<Rigidbody2D>();
            steeringManager.Init(enemyParams.maxVelocity, enemyParams.maxForce);
            initialPosition = transform.position;
            detectedUnits = new Collider2D[1];
            contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Minions"));
        }

        void Update()
        {
            if (!isActive)
                return;

            detectionCollider.transform.right = rigidBody.velocity.normalized;

            steeringManager.AvoidCollisions(enemyParams.seeAheadDistance, enemyParams.maxAvoidForce);

            if(agrroedTarget != null)
            {
                steeringManager.Pursue(agrroedTarget);
                return;
            }

            int detectedUnitsCount = detectionCollider.OverlapCollider(contactFilter, detectedUnits);
            if(detectedUnitsCount > 0)
            {
                agrroedTarget = detectedUnits[0].gameObject.GetComponent<Rigidbody2D>();
                if(agrroedTarget == null)
                {
                    Debug.LogWarning("Enenmy is trying to aggro to target with no rigidbody");
                }
                return;
            }

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

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var minionController = collision.gameObject.GetComponent<MinionController>();
            if (minionController == null)
                return;
            minionController.Die();
            agrroedTarget = null;
        }
    }
}