using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Minion
{
    [CreateAssetMenu(fileName = "MinionParamsSO", menuName = "Scriptable Objects/Minions/Minion Params")]
    public class MinionParamsSO : ScriptableObject
    {
        public float maxVelocity;
        public float maxForce;
        public float seeAheadDistance;
        public float maxAvoidForce;
        public float separationRadius = 1f;
        public float separationSpeed = 5f;
    }
}

