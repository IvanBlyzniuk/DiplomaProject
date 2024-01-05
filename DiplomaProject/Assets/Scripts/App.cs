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

        void Start()
        {
            inputSystem.Init(objectsContainer.CameraFollowTarget);
        }

    }
}