using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    public class Destroyable : MonoBehaviour
    {
        public void Destroy(float timeToWait)
        {
            Destroy(gameObject, timeToWait);
        }
    }
}

