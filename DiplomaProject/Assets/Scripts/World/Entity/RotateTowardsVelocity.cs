using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    public class RotateTowardsVelocity : MonoBehaviour
    {
        private Rigidbody2D rigidBody;

        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            if (rigidBody == null)
                rigidBody = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        }


        void Update()
        {
            if(rigidBody.velocity != Vector2.zero)
            {
                transform.up = Vector3.Lerp(transform.up, rigidBody.velocity.normalized, Time.deltaTime * 5);
            }
                
        }
    }
}
