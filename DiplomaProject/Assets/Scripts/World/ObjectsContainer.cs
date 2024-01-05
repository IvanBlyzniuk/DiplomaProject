using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField] private CameraTarget cameraFollowTarget;

        public CameraTarget CameraFollowTarget => cameraFollowTarget;
    }
}

