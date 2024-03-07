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
        }


        void Update()
        {
            if(rigidBody.velocity != Vector2.zero)
                transform.up = rigidBody.velocity.normalized;
        }
    }
}
