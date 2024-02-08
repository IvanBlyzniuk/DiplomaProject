using App.World.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class PlayingState : IState
    {
        private InputSystem inputSystem;
        private IResettable[] resettables;

        public PlayingState(InputSystem inputSystem, IResettable[] resettables)
        {
            this.resettables = resettables;
            this.inputSystem = inputSystem;
        }

        public void Enter()
        {
            inputSystem.InputState = InputStates.Playing;
            foreach (var resettable in resettables)
            {
                resettable.Activate();
            }
        }

        public void Exit()
        {
            foreach(var resettable in resettables)
            {
                resettable.ResetState();
            }
        }

        public void Update()
        {
            
        }
    }
}
