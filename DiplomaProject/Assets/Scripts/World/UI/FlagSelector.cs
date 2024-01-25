using App.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.UI
{
    public class FlagSelector : MonoBehaviour
    {
        private IPlacementSystem placementSystem;

        [SerializeField] private GameObject flagToSelect;

        public void Init(IPlacementSystem placementSystem)
        {
            this.placementSystem = placementSystem;
        }

        public void Select()
        {
            placementSystem.ObjectToPLace = flagToSelect;
        }
    }
}

