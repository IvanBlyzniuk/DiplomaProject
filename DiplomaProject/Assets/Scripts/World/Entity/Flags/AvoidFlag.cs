using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public class AvoidFlag : MonoBehaviour, IFlag
    { 
    
        public void AddOrder(ISet<MinionController> minions)
        {
            var hoveredObject = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Enemies"));
            if (hoveredObject == null)
            {
                foreach(MinionController minion in minions)
                {
                    minion.AddFleeTarget(hoveredObject.gameObject);
                }
            }
            else
            {
                //TODO: add flag to enemy
                foreach (MinionController minion in minions)
                {
                    minion.AddEvadeTarget(hoveredObject.gameObject);
                }
            }
        }

        public bool CheckPlacementValidity(Vector2 position)
        {
            return !Physics2D.OverlapPoint(position, LayerMask.GetMask(new[] { "Walls", "Obstacles" }));
        }
    }
}
