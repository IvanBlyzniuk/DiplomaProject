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

        void Start()
        {
            unitSelectionSystem.Init(objectsContainer.MainCamera, objectsContainer.SelectorImage);
            inputSystem.Init(unitSelectionSystem, objectsContainer.MainCamera, objectsContainer.CameraFollowTarget);
        }

    }
}