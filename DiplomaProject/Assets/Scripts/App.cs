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

        void Start()
        {
            unitSelectionSystem.Init(objectsContainer.MainCamera, objectsContainer.SelectorImage);
            inputSystem.Init(unitSelectionSystem, flagSystem, objectsContainer.MainCamera, objectsContainer.CameraFollowTarget);
            flagSystem.Init(inputSystem, objectsContainer.FlagPlacementPreview, objectsContainer.MainCamera);

            foreach (var flagSelector in objectsContainer.FlagSelectors)
            {
                flagSelector.Init(flagSystem);
            }
        }
    }
}