using App.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace App.World.UI
{
    public class FlagSelector : MonoBehaviour
    {
        private IPlacementSystem placementSystem;

        [SerializeField] private int allowedFlagsCount;
        [SerializeField] private GameObject flagToSelect;
        [SerializeField] private TextMeshProUGUI countText;

        public int AllowedFlagsCount 
        { 
            get => allowedFlagsCount;
            set
            {
                allowedFlagsCount = value;
                countText.text = $"x {value}";
            }
        }

        public void Init(IPlacementSystem placementSystem)
        {
            this.placementSystem = placementSystem;
            countText.text = $"x {allowedFlagsCount}";
        }

        public void Select()
        {
            placementSystem.ObjectToPLace = flagToSelect;
            placementSystem.ActiveSelector = this;
        }

    }
}

