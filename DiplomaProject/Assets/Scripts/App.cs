using App.Systems;
using App.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class App : MonoBehaviour
    {

        [SerializeField] private ObjectsContainer objectsContainer;
        [SerializeField] private InputSystem inputSystem;
        [SerializeField] private UnitSelectionSystem unitSelectionSystem;
        [SerializeField] private FlagSystem flagSystem;
        [SerializeField] private GameStateSystem gameStateSystem;

        void Start()
        {
            unitSelectionSystem.Init(objectsContainer.MainCamera, objectsContainer.SelectorImage);
            inputSystem.Init(unitSelectionSystem, flagSystem, objectsContainer.MainCamera, objectsContainer.CameraFollowTarget);
            flagSystem.Init(inputSystem, unitSelectionSystem, objectsContainer.FlagPlacementPreview, objectsContainer.MainCamera);
            gameStateSystem.Init( inputSystem, objectsContainer.gameObject);
            objectsContainer.GameStateChanger.Init(gameStateSystem);
            foreach (var flagSelector in objectsContainer.FlagSelectors)
            {
                flagSelector.Init(flagSystem);
            }
        }
    }
}