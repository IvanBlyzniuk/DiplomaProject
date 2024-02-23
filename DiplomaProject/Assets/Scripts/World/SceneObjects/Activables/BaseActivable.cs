using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.SceneObjects.Activables
{
    public abstract class BaseActivable : MonoBehaviour
    {
        private int inputCount = 0;
        private int activeCount = 0;
        protected abstract void OnActivate();
        protected abstract void OnDeactivate();

        public virtual void OnReset() { }

        public void AddActuator()
        {
            inputCount++;
        }

        public void Activate()
        {
            activeCount++;
            if (inputCount == activeCount)
                OnActivate();
        }

        public void Deactivate()
        {
            if (inputCount == activeCount)
                OnDeactivate();
            activeCount--;
        }
    }
}
