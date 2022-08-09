namespace archive.Map
{
    using Assets.Scripts.src.Common.Contracts;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Tilemaps;

    public class OldMapManager : Singleton<OldMapManager>
    {
        public Grid Grid;
        public Tilemap Tilemap;
        public List<Vector3> AvailablePositions;
        private bool mapIsGenerated = false;

        public UnityEvent generateMap;

        public Vector3Int WorldToCell(Vector3 worldPoint)
        {
            return Grid.WorldToCell(worldPoint);
        }

        public Vector3 CellToWorld(Vector3Int cellPoint)
        {
            return Grid.CellToWorld(cellPoint);
        }

        public bool WorldContainsCell(Vector3Int cellPosition)
        {
            return Tilemap.HasTile(cellPosition);
        }

        #region UnityLifecycle

        private void Start()
        {
        }

        private void Update()
        {
            if (!mapIsGenerated)
            {
                FillAvailablePosition();
            }
        }

        private void FillAvailablePosition()
        {
            foreach (var position in Tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int currentPlace = new Vector3Int(position.x, position.y, position.z);
                Vector3 worldPoint = Tilemap.CellToWorld(currentPlace);
                if (Tilemap.HasTile(currentPlace))
                {
                    AvailablePositions.Add(worldPoint);
                }
            }
            mapIsGenerated = true;
            generateMap.Invoke();
        }

        private void Awake()
        {
            AvailablePositions = new List<Vector3>();
        }

        #endregion UnityLifecycle
    }
}