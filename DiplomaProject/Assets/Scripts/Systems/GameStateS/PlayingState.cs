using App.World.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class PlayingState : IState
    {
        private IResettable[] resettables;

        public PlayingState(IResettable[] resettables)
        {
            this.resettables = resettables;
        }

        public void Enter()
        {
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
