using App.World.Entity.Minion;
using App.World.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity.Flags
{
    public abstract class BaseFlag: MonoBehaviour
    {
        protected FlagSelector selector;
        //[SerializeField] private OrderType orderType;
        //public OrderType OrderType => orderType;
        public abstract bool CheckPlacementValidity(Vector2 position);
        public virtual void Init(ISet<MinionController> minions, FlagSelector selector)
        {
            this.selector = selector;
            selector.AllowedFlagsCount--;
            AddOrder(minions);
        }

        protected abstract void AddOrder(ISet<MinionController> minions);

        protected abstract void RemoveOrder();

        public virtual void RemoveFlag()
        {
            selector.AllowedFlagsCount++;
            RemoveOrder();
            Destroy(gameObject);
        }

        //public GameObject gameObject { get; }
    }

    //[System.Serializable]
    //public enum OrderType
    //{
    //    Seek,
    //    Avoid,
    //    FollowLeader,
    //}
}

