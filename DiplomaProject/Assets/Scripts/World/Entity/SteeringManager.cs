using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SteeringManager : MonoBehaviour
    {
        private Vector2 steering;
        private Rigidbody2D rigidBody;

        [SerializeField] float maxVelocity;
        [SerializeField] float maxForce; //static const?

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
        }

        public void Seek(GameObject target, float slowingRadius = 0f)
        {
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
    }
}

