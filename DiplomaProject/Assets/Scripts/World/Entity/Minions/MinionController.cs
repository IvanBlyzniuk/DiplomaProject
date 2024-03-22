using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Minion
{
    [RequireComponent(typeof(SteeringManager))]
    public class MinionController : MonoBehaviour, IResettable
    {
        private static readonly Color halfTransparrentGreen = new Color(0f, 1f, 0f, 0.5f);

        private bool isActive = false;
        private SteeringManager steeringManager;
        private SpriteRenderer spriteRenderer;
        private RotateTowardsVelocity rotateTowardsVelocity;
        private Rigidbody2D rigidBody;
        private Animator animator;

        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private List<GameObject> seekTargets = new List<GameObject>();
        private List<GameObject> fleeTargets = new List<GameObject>();
        private List<Rigidbody2D> evadeTargets = new List<Rigidbody2D>();
        private int currentSeekTargetIndex = 0;

        [SerializeField] private MinionParamsSO minionParams;

        public List<GameObject> SeekTargets => seekTargets;
        public List<GameObject> FleeTargets => fleeTargets;
        public List<Rigidbody2D> EvadeTargets => evadeTargets;

        public Rigidbody2D Leader { get; set; }

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            rotateTowardsVelocity = GetComponent<RotateTowardsVelocity>();
            rigidBody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            steeringManager.Init(minionParams.maxVelocity, minionParams.maxForce);
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
        void Update()
        {
            if (rigidBody.velocity.magnitude > minionParams.maxVelocity / 5f)
            {
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            if (!isActive)
                return;
            steeringManager.AvoidCollisions(minionParams.seeAheadDistance, minionParams.maxAvoidForce);
            if(currentSeekTargetIndex < seekTargets.Count)
            {
                var currentSeekTarget = seekTargets[currentSeekTargetIndex];
                steeringManager.Seek(currentSeekTarget);
                if (Vector2.Distance(transform.position, currentSeekTarget.transform.position) < minionParams.seekTargetReachedDistance)
                {
                    currentSeekTargetIndex++;
                }
            }

            foreach (var fleeTarget in fleeTargets)
            {
                if(Vector2.Distance(transform.position, fleeTarget.transform.position) < minionParams.maxFleeDistance)
                    steeringManager.Flee(fleeTarget);
            }

            foreach(var evadeTarget in evadeTargets)
            {
                if (Vector2.Distance(transform.position, evadeTarget.transform.position) < minionParams.maxFleeDistance)
                {
                    steeringManager.Evade(evadeTarget);
                    Debug.Log("Evading");
                }
                    
            }

            if(Leader != null && Leader.gameObject != gameObject)
            {
                steeringManager.FollowLeader(Leader, 2f, 1f, minionParams.separationRadius, minionParams.separationSpeed, 1f);
            }
            //if (shouldWander)
            //    steeringManager.Wander(minionParams.maxVelocity /2 , minionParams.maxVelocity / 3, 10);
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            rotateTowardsVelocity.enabled = false;
            steeringManager.ResetStearing();
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            currentSeekTargetIndex = 0;
            isActive = false;
        }

        public void Activate()
        {
            isActive = true;
            rotateTowardsVelocity.enabled = true;
            //Debug.Log("Activated");
        }

        public void Die()
        {
            //TODO: play animation
            gameObject.SetActive(false);
        }

        public void Select()
        {
            spriteRenderer.color = halfTransparrentGreen;
        }

        public void Deselect()
        {
            spriteRenderer.color = Color.white;
        }

        //For debugging purposes
        public override string ToString()
        {
            return gameObject.name;
        }
    }
}

