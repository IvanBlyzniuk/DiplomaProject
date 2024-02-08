using App.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.UI
{
    public class GameStateChanger : MonoBehaviour
    {
        private IGameStateSystem gameStateSystem;
        private bool isPlaying = false;
        public void Init(IGameStateSystem gameStateSystem)
        {
            this.gameStateSystem = gameStateSystem;
        }

        public void ChangeState()
        {
            if (isPlaying)
                gameStateSystem.GoToPlanningState();
            else
                gameStateSystem.GoToPlayingState();
            isPlaying = !isPlaying;
        }
    }
}
