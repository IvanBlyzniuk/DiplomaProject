using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.World.SceneObjects.Activables
{
    [CreateAssetMenu(fileName = "WirelookupTable", menuName = "Scriptable Objects/Wires/WireLookupTable", order = 1)]
    public class WireLookupTable : ScriptableObject
    {
        [System.Serializable]
        public class TilePairEntry
        {
            public Tile original;
            public Tile active;
        }
        public List<TilePairEntry> tilePairs;
    }
}
