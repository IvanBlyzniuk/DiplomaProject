using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.World.SceneObjects.Activables
{
    public class Wire : BaseActivable
    {
        private Tilemap tilemap;
        [SerializeField] private WireLookupTable wireLookupTable; 

        private void Awake()
        {
            tilemap = GetComponent<Tilemap>();
            if (tilemap == null)
                Debug.LogWarning($"Tilemap not found on Wire: {gameObject.name}");
        }

        protected override void OnActivate()
        {
            foreach (var tilePair in wireLookupTable.tilePairs)
            {
                tilemap.SwapTile(tilePair.original, tilePair.active);
            }
        }

        protected override void OnDeactivate()
        {
            foreach (var tilePair in wireLookupTable.tilePairs)
            {
                tilemap.SwapTile(tilePair.active, tilePair.original);
            }
        }

    }
}
