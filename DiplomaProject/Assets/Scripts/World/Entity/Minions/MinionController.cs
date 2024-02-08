using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Minion
{
    [RequireComponent(typeof(SteeringManager))]
    public class MinionController : MonoBehaviour, IResettable
    {
        private bool isActive = false;
        private SteeringManager steeringManager;

        private Vector3 initialPosition;

        private Rigidbody2D evadeTargetBody;
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
            steeringManager.Init(minionParams.maxVelocity, minionParams.maxForce);
            initialPosition = transform.position;
        }
        void Update()
        {
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
                    steeringManager.Evade(evadeTarget);
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
            transform.position = initialPosition;
            steeringManager.ResetStearing();
            currentSeekTargetIndex = 0;
            isActive = false;
        }

        public void Activate()
        {
            isActive = true;
            //Debug.Log("Activated");
        }

        //For debugging purposes
        public override string ToString()
        {
            return gameObject.name;
        }
    }
}

