using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class PlanningState : IState
    {
        private InputSystem inputSystem;

        public PlanningState(InputSystem inputSystem)
        {
            this.inputSystem = inputSystem;
        }

        public void Enter()
        {
            inputSystem.InputState = InputStates.Empty;
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
