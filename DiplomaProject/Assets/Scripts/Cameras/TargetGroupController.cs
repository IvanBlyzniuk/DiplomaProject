using App.World;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Cameras
{
    public class TargetGroupController : MonoBehaviour
    {

        [SerializeField]
        private CinemachineTargetGroup targetGroup;
        [SerializeField]
        private ObjectsContainer container;
        private CameraTarget followTarget;
        void Start()
        {
            followTarget = container.CameraFollowTarget;
            SetTargets();
        }

        private void SetTargets()
        {
            CinemachineTargetGroup.Target targetGroupPlayer = new CinemachineTargetGroup.Target { weight = 1, radius = 7, target = followTarget.transform };
            CinemachineTargetGroup.Target[] targets = new CinemachineTargetGroup.Target[] { targetGroupPlayer };
            targetGroup.m_Targets = targets;
        }
    }

}
