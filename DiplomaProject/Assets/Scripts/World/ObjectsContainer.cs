using App.World.SceneObjects;
using App.World.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField] private CameraTarget cameraFollowTarget;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private RectTransform selectorImage;
        [SerializeField] private SpriteRenderer flagPlacementPreview;
        [SerializeField] private GameStateChanger gameStateChanger;
        [SerializeField] private LevelEndingTrigger levelEndingTrigger;
        [SerializeField] private Image blackOverlay; 
        [SerializeField] private List<FlagSelector> flagSelectors;
        public CameraTarget CameraFollowTarget => cameraFollowTarget;
        public Camera MainCamera => mainCamera;
        public RectTransform SelectorImage => selectorImage;
        public SpriteRenderer FlagPlacementPreview => flagPlacementPreview;
        public GameStateChanger GameStateChanger => gameStateChanger;
        public LevelEndingTrigger LevelEndingTrigger => levelEndingTrigger;
        public Image BlackOverlay => blackOverlay;
        public List<FlagSelector> FlagSelectors => flagSelectors;
    }
}

