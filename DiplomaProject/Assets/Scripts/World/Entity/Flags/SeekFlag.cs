using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace App.World.Entity.Flags
{
    public class SeekFlag : BaseFlag
{
        private List<MinionController> affectedMinions;
        protected override void AddOrder(ISet<MinionController> minions)
        {
            affectedMinions = new List<MinionController>(minions);
            foreach (MinionController minion in affectedMinions)
            {
                minion.SeekTargets.Add(gameObject);
            }
        }

        public override bool CheckPlacementValidity(Vector2 position)
        {
            return !Physics2D.OverlapPoint(position, LayerMask.GetMask(new[] { "Walls", "Obstacles" }));
        }

        protected override void RemoveOrder()
        {
            foreach (MinionController minion in affectedMinions)
            {
                minion.SeekTargets.Remove(gameObject);
            }
        }
    }
}
