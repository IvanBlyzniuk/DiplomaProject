using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SteeringManager : MonoBehaviour
    {
        #region debug_utils
        private GameObject lastSeekTarget;
        private float lastSeekTargetSlowingRadius;
        #endregion

        private float wanderAngle = 0f;
        private Vector2 steering;
        private Rigidbody2D rigidBody;
        private MinionController[] minions;

        [SerializeField] private float maxVelocity;
        [SerializeField] private float maxForce; //static const?

        public float MaxVelocity => maxVelocity;

        void Start()
        {
            minions = FindObjectsOfType<MinionController>();
            rigidBody = GetComponent<Rigidbody2D>();
            steering = Vector2.zero;
        }


        void LateUpdate()
        {
            steering = Vector2.ClampMagnitude(steering, maxForce);
            rigidBody.AddForce(steering);
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelocity);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + rigidBody.velocity);
            if(lastSeekTarget != null && lastSeekTarget.activeSelf)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(lastSeekTarget.transform.position, lastSeekTargetSlowingRadius);
            }
        }

        public void Seek(Vector3 targetPosition, float slowingRadius = 0f)
        {
            Vector2 force;
            Vector2 desired = (targetPosition - transform.position);
            float distance = desired.magnitude;
            desired.Normalize();
            if (distance <= slowingRadius)
            {
                desired *= maxVelocity * distance / slowingRadius;
            }
            else
            {
                desired *= maxVelocity;
            }

            force = desired - rigidBody.velocity;

            steering += force;
        }

        public void Seek(GameObject target, float slowingRadius = 0f)
        {
            lastSeekTarget = target;
            lastSeekTargetSlowingRadius = slowingRadius;
            Seek(target.transform.position, slowingRadius);
        }

        public void Flee(GameObject target)
        {
            Flee(target.transform.position);
        }

        public void Flee(Vector3 targetPosition)
        {
            Vector2 force;
            Vector2 desired = (transform.position - targetPosition);

            desired.Normalize();
            desired *= maxVelocity;

            force = desired - rigidBody.velocity;

            steering += force;
        }

        public void Wander(float wanderCircleDistance, float wanderCircleRadius, float wanderAngleChange)
        {
            Vector2 circleCentre = rigidBody.velocity;
            circleCentre.Normalize();
            circleCentre *= wanderCircleDistance;

            Vector3 displacement = Vector3.up;
            displacement *= wanderCircleRadius;

            Quaternion rotation = Quaternion.Euler(0, 0, wanderAngle);

            displacement = rotation * displacement;

            wanderAngle += (Random.value - 0.5f) * wanderAngleChange;

            Vector2 wanderForce = circleCentre + (Vector2)displacement;

            steering += wanderForce;
        }

        public void Evade(Rigidbody2D target)
        {
            Vector2 distance = target.transform.position - transform.position;
            float timeAhead = distance.magnitude / maxVelocity;
            Vector2 futurePosition = (Vector2)target.transform.position + target.velocity * timeAhead;
            Flee(futurePosition);
        }

        public void HandleSeparation()
        {
            Vector3 force = Vector3.zero;
            int neighborCount = 0;
            foreach (var minion in minions)
            {
                if (minion.gameObject != gameObject 
                    && Vector2.Distance(transform.position, minion.transform.position) <= minion.SeparationRadius)
                {
                    force += minion.transform.position - transform.position;
                    neighborCount++;
                }
            }
            if (neighborCount > 0)
            {
                force /= -neighborCount;
            }
            force.Normalize();
            force *= minions[0].SeparationSpeed;
            steering += (Vector2)force;
        }

        public void FollowLeader(Rigidbody2D leader, float leaderBehindDist, float leaderSightRadius, float slowingRadius = 0f)
        {
            Vector2 force = Vector2.zero;
            Vector2 tv = leader.velocity.normalized * leaderBehindDist;
            Vector2 behindPosition = leader.position - tv;
            Vector2 aheadPosition = leader.position + tv;

            if(Vector2.Distance(aheadPosition, transform.position) <= leaderSightRadius || Vector2.Distance(leader.transform.position, transform.position) <= leaderSightRadius)
            {
                Evade(leader);
            }

            Seek(behindPosition, slowingRadius);

            HandleSeparation();
        }
    }
}

