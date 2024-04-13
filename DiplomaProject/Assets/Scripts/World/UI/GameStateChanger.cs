using App.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.World.UI
{
    public class GameStateChanger : MonoBehaviour
    {
        private IGameStateSystem gameStateSystem;
        private Image image;
        private bool isPlaying = false;
        private Sprite preparingSprite;
        [SerializeField] private Sprite playingSprite;
        public void Init(IGameStateSystem gameStateSystem)
        {
            this.gameStateSystem = gameStateSystem;
            image = GetComponent<Image>();
            preparingSprite = image.sprite;
        }

        public void ChangeState()
        {
            isPlaying = !isPlaying;
            if (isPlaying)
            {
                gameStateSystem.GoToPlayingState();
                image.sprite = playingSprite;
            }
            else
            {
                gameStateSystem.GoToPlanningState();
                image.sprite = preparingSprite;
            }
                
            
        }
    }
}
