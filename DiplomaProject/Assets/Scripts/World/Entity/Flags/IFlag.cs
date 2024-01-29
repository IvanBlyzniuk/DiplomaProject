using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public interface IFlag
    {
        //[SerializeField] private OrderType orderType;
        //public OrderType OrderType => orderType;
        public bool CheckPlacementValidity(Vector2 position);
        public void AddOrder(ISet<MinionController> minions);

        public GameObject gameObject { get; }
    }

    //[System.Serializable]
    //public enum OrderType
    //{
    //    Seek,
    //    Avoid,
    //    FollowLeader,
    //}
}

