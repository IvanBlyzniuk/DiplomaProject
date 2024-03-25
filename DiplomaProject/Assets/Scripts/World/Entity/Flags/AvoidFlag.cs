using App.World.Entity.Enemy;
using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public class AvoidFlag : BaseFlag
    {
        private List<MinionController> affectedMinions;
        private Rigidbody2D targetBody;
        private EnemyController enemyController;
        protected override void AddOrder(ISet<MinionController> minions)
        {
            affectedMinions = new List<MinionController>(minions);
            var hoveredObject = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Enemies"));
            if (hoveredObject == null)
            {
                foreach(MinionController minion in affectedMinions)
                {
                    minion.FleeTargets.Add(gameObject);
                }
            }
            else
            {
                targetBody = hoveredObject.GetComponent<Rigidbody2D>();
                enemyController = hoveredObject.GetComponent<EnemyController>();
                if (targetBody == null || enemyController == null)
                {
                    Debug.LogWarning("Trying to avoid enemy with no RigidBody or controller");
                    return;
                }
                enemyController.EnableMark();
                GetComponent<SpriteRenderer>().enabled = false;
                foreach (MinionController minion in affectedMinions)
                {   
                    minion.EvadeTargets.Add(targetBody);
                }
            }
        }

        public override bool CheckPlacementValidity(Vector2 position)
        {
            return !Physics2D.OverlapPoint(position, LayerMask.GetMask(new[] { "Walls", "Obstacles" }));
        }

        protected override void RemoveOrder()
        {
            if(targetBody != null)
            {
                enemyController.DisableMark();
                foreach (var minion in affectedMinions)
                {
                    minion.EvadeTargets.Remove(targetBody);
                }
            }
            else
            {
                foreach(var minion in affectedMinions)
                {
                    minion.FleeTargets.Remove(gameObject);
                }
            }
        }
    }
}
