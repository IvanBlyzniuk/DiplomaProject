using App.World.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public interface IPlacementSystem
    {
        public GameObject ObjectToPLace { get; set; }
        public FlagSelector ActiveSelector { get; set; }
    }
}
