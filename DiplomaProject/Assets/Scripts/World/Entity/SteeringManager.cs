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

        [SerializeField] private float maxVelocity;
        [SerializeField] private float maxForce; //static const?

        public float MaxVelocity => maxVelocity;

        void Start()
        {
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

        public void Seek(GameObject target, float slowingRadius = 0f)
        {
            lastSeekTarget = target;
            lastSeekTargetSlowingRadius = slowingRadius;

            Vector2 force;
            Vector2 desired = (target.transform.position - transform.position);
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

        public void Evade(GameObject target)
        {
            Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
            if(targetBody == null)
            {
                Debug.LogWarning("Avoiding target with no rigidbody");
                return;
            }
            Vector2 distance = target.transform.position - transform.position;
            float timeAhead = distance.magnitude / maxVelocity;
            Vector2 futurePosition = (Vector2)target.transform.position + targetBody.velocity * timeAhead;
            Flee(futurePosition);
        }
    }
}

