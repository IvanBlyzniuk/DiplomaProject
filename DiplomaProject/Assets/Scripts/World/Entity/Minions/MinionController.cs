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
        private const float separationRadius = 1f;
        private const float separationSpeed = 5f;

        [SerializeField] private GameObject seekTarget;
        [SerializeField] private GameObject fleeTarget;
        [SerializeField] private GameObject evadeTarget;
        [SerializeField] private GameObject leader;
        [SerializeField] private bool shouldWander;
        [SerializeField] private float seeAheadDistance;
        [SerializeField] private float avoidForceMultiplier;

        public float SeparationRadius => separationRadius;
        public float SeparationSpeed => separationSpeed;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            if (evadeTarget != null)
                evadeTargetBody = evadeTarget.GetComponent<Rigidbody2D>();
            if(leader != null)
                leaderBody = leader.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            steeringManager.AvoidCollisions(seeAheadDistance, avoidForceMultiplier);
            if(seekTarget != null)
                steeringManager.Seek(seekTarget, 1f);
            if(fleeTarget != null)
                steeringManager.Flee(fleeTarget);
            if(evadeTarget != null)
                steeringManager.Evade(evadeTargetBody);
            if (leader != null)
                steeringManager.FollowLeader(leaderBody, 2f, 1f, separationRadius, 1f);
            if (shouldWander)
                steeringManager.Wander(steeringManager.MaxVelocity /2 , steeringManager.MaxVelocity / 3, 10);
        }
    }
}

