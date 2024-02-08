using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public interface IGameStateSystem
    {
        public void GoToPlayingState();
        public void GoToPlanningState();
    }
}
