using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public class LeaderFlag : MonoBehaviour, IFlag
    {
        private List<MinionController> affectedMinions;
        private Rigidbody2D leaderBody;
        public void AddOrder(ISet<MinionController> minions)
        {
            affectedMinions = new List<MinionController>(minions);
            var hoveredObject = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Minions"));
            leaderBody = hoveredObject.GetComponent<Rigidbody2D>();
            foreach (var minion in affectedMinions)
            {
                minion.Leader = leaderBody;
            }
        }

        public bool CheckPlacementValidity(Vector2 position)
        {
            return Physics2D.OverlapPoint(position, LayerMask.GetMask("Minions"));
        }

        public void RemoveOrder()
        {
            foreach (var minion in affectedMinions)
            {
                minion.Leader = null;
            }
        }
    }
}
