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

        void Start()
        {
            steeringManager = GetComponent<SteeringManager>();
        }
        void Update()
        {
            steeringManager.Seek(seekTarget, 1f);
        }
    }
}

