using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World
{
    public class CameraTarget : MonoBehaviour
    {
        private Rigidbody2D rigidBody;

        [SerializeField] private float movementSpeed;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 direction)
        {
            
            rigidBody.velocity = direction * movementSpeed;
        }
    }
}

