using App.World.SceneObjects.Activables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.SceneObjects.Actuators
{
    public class BaseActuator : MonoBehaviour
    {
        [SerializeField] private List<BaseActivable> activables;

        private void Start()
        {
            foreach (var activable in activables)
            {
                activable.AddActuator();
            }
        }

        public void Activate()
        {
            foreach (var activable in activables)
            {
                activable.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var activable in activables)
            {
                activable.Deactivate();
            }
        }
    }
}
