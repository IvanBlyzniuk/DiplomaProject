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
        private UnitSelectionSystem unitSelectionSystem;

        public PlayingState(InputSystem inputSystem, IResettable[] resettables, UnitSelectionSystem unitSelectionSystem)
        {
            this.resettables = resettables;
            this.inputSystem = inputSystem;
            this.unitSelectionSystem = unitSelectionSystem;
        }

        public void Enter()
        {
            unitSelectionSystem.ClearSelection();
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
