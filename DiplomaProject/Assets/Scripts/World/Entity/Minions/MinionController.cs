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

        [SerializeField] private MinionParamsSO minionParams;

        [Header("Debug fields")]
        [SerializeField] private GameObject seekTarget;
        [SerializeField] private GameObject fleeTarget;
        [SerializeField] private GameObject evadeTarget;
        [SerializeField] private GameObject leader;
        [SerializeField] private bool shouldWander;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
            steeringManager.Init(minionParams.maxVelocity, minionParams.maxForce);
            if (evadeTarget != null)
                evadeTargetBody = evadeTarget.GetComponent<Rigidbody2D>();
            if(leader != null)
                leaderBody = leader.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            steeringManager.AvoidCollisions(minionParams.seeAheadDistance, minionParams.maxAvoidForce);
            if(seekTarget != null)
                steeringManager.Seek(seekTarget, 1f);
            if(fleeTarget != null)
                steeringManager.Flee(fleeTarget);
            if(evadeTarget != null)
                steeringManager.Evade(evadeTargetBody);
            if (leader != null)
                steeringManager.FollowLeader(leaderBody, 2f, 1f, minionParams.separationRadius, minionParams.separationSpeed, 1f);
            if (shouldWander)
                steeringManager.Wander(minionParams.maxVelocity /2 , minionParams.maxVelocity / 3, 10);
        }
    }
}

