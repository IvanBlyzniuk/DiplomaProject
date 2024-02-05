using App.World.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class GameStateSystem : MonoBehaviour, IGameStateSystem
    {
        private GameObject worldParentObject;
        private IResettable[] resettables;

        private StateMachine stateMachine;
        private PlayingState playingState;
        private PlanningState planningState;

        public void Init(GameObject worldParentObject)
        {
            this.worldParentObject = worldParentObject;
            stateMachine = new StateMachine();
            playingState = new PlayingState(resettables);
            planningState = new PlanningState();
            stateMachine.Initialize(planningState);
        }

        void Awake()
        {
            resettables = worldParentObject.GetComponentsInChildren<IResettable>();
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
