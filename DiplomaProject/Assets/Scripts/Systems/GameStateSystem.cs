using App.World.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class GameStateSystem : MonoBehaviour, IGameStateSystem
    {
        private InputSystem inputSystem;
        private GameObject worldParentObject;
        private IResettable[] resettables;

        private StateMachine stateMachine;
        private PlayingState playingState;
        private PlanningState planningState;

        public void Init(InputSystem inputSystem, GameObject worldParentObject, UnitSelectionSystem unitSelectionSystem)
        {
            this.inputSystem = inputSystem;
            this.worldParentObject = worldParentObject;
            resettables = this.worldParentObject.GetComponentsInChildren<IResettable>();
            stateMachine = new StateMachine();
            playingState = new PlayingState(inputSystem, resettables, unitSelectionSystem);
            planningState = new PlanningState(inputSystem);
            stateMachine.Initialize(planningState);
        }
        public void GoToPlayingState()
        {
            stateMachine.ChangeState(playingState);
        }

        public void GoToPlanningState()
        {
            stateMachine.ChangeState(planningState);
        }
    }
}
