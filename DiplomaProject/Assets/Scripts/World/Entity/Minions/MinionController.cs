using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Minion
{
    [RequireComponent(typeof(SteeringManager))]
    public class MinionController : MonoBehaviour
    {
        private SteeringManager steeringManager;

        [SerializeField] private GameObject seekTarget;
        [SerializeField] private GameObject fleeTarget;
        [SerializeField] private GameObject evadeTarget;
        [SerializeField] private bool shouldWander;

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
        }
        void Update()
        {
            if(seekTarget != null)
                steeringManager.Seek(seekTarget, 1f);
            if(fleeTarget != null)
                steeringManager.Flee(fleeTarget);
            if(evadeTarget != null)
                steeringManager.Evade(evadeTarget);
            if (shouldWander)
                steeringManager.Wander(steeringManager.MaxVelocity /2 , steeringManager.MaxVelocity / 3, 10);
        }
    }
}

