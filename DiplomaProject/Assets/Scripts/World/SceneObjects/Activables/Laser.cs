using App.World.Entity.Enemy;
using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.SceneObjects.Activables
{
    public class Laser : BaseActivable
    {
        private const float MAX_RAYCAST_DISTANCE = 1000f;
        private LineRenderer lineRenderer;
        private bool isActive;
        private LayerMask layerMask;
        [SerializeField] private bool activeByDefault;

        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            layerMask = LayerMask.GetMask(new string[]{ "Obstacles", "Walls", "Minions", "Enemies" });
            isActive = activeByDefault;
            lineRenderer.enabled = isActive;
            lineRenderer.positionCount = 2;
        }

        void Update()
        {
            if (!isActive)
                return;
            var raycastHit = Physics2D.Raycast(transform.position, transform.up, MAX_RAYCAST_DISTANCE, layerMask);
            if (raycastHit)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, raycastHit.point);
                var minion = raycastHit.collider.gameObject.GetComponent<MinionController>();
                minion?.Die();
                var enemy = raycastHit.collider.gameObject.GetComponent<EnemyController>();
                //enemy?.Die();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up);
        }

        protected override void OnActivate()
        {
            isActive = !activeByDefault;
            lineRenderer.enabled = isActive;
        }

        protected override void OnDeactivate()
        {
            isActive = activeByDefault;
            lineRenderer.enabled = isActive;
        }

        
    }
}
