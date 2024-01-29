using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Minion
{
    [RequireComponent(typeof(SteeringManager))]
    public class MinionController : MonoBehaviour
    {
        private SteeringManager steeringManager;
        private Rigidbody2D evadeTargetBody;
        private Rigidbody2D leaderBody;
        private List<GameObject> seekTargets = new List<GameObject>();
        private List<GameObject> fleeTargets = new List<GameObject>();
        private List<Rigidbody2D> evadeTargets = new List<Rigidbody2D>();
        private int currentSeekTargetIndex = 0;

        [SerializeField] private MinionParamsSO minionParams;

        //[Header("Debug fields")]
        //[SerializeField] private GameObject leader;
        //[SerializeField] private bool shouldWander;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            steeringManager.Init(minionParams.maxVelocity, minionParams.maxForce);
            //if(leader != null)
            //    leaderBody = leader.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
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
            //if (leader != null)
            //    steeringManager.FollowLeader(leaderBody, 2f, 1f, minionParams.separationRadius, minionParams.separationSpeed, 1f);
            //if (shouldWander)
            //    steeringManager.Wander(minionParams.maxVelocity /2 , minionParams.maxVelocity / 3, 10);
        }

        public void AddSeekTarget(GameObject target)
        {
            seekTargets.Add(target);
        }

        internal void AddFleeTarget(GameObject target)
        {
            fleeTargets.Add(target);
        }

        internal void AddEvadeTarget(GameObject target)
        {
            var targetBody = target.GetComponent<Rigidbody2D>();
            if (targetBody == null)
            {
                Debug.LogWarning("Trying to avoid enemy with no RigidBody");
                return;
            }
            evadeTargets.Add(targetBody);
        }

        //For debugging purposes
        public override string ToString()
        {
            return gameObject.name;
        }
    }
}

