using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField] private CameraTarget cameraFollowTarget;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private RectTransform selectorImage;

        public CameraTarget CameraFollowTarget => cameraFollowTarget;
        public Camera MainCamera => mainCamera;
        public RectTransform SelectorImage => selectorImage;
    }
}

