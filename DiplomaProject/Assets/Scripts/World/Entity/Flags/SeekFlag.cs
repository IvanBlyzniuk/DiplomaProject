using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public class SeekFlag : MonoBehaviour, IFlag
{
        public void AddOrder(ISet<MinionController> minions)
        {
            foreach (MinionController minion in minions)
            {
                minion.AddSeekTarget(gameObject);
            }
        }

        public bool CheckPlacementValidity(Vector2 position)
        {
            return !Physics2D.OverlapPoint(position, LayerMask.GetMask(new[] { "Walls", "Obstacles" }));
        }
    }
}
