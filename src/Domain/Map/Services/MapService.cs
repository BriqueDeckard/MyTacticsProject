using Assets.Scripts.src.Common.Contracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.src.Domain.Map.Services
{
    public class MapService : Singleton<MapService>
    {
        public Grid Grid;
        public Tilemap Tilemap;
        public Tilemap HighlighTilemap;
        public Dictionary<Vector3Int, Vector3> AvailableLocations;
        public Tile OverlayTile;
        private Vector3Int PreviousCellPosition;

        public Vector3Int GetCellPosition(Vector3 vector3)
        {
            return Instance.Grid.WorldToCell(vector3);
        }

        public bool TileIsAvailable(Vector3Int position)
        {
            var worldPosition = Grid.CellToWorld(position);
            return Instance.AvailableLocations.Keys.Any(x => (x.x == worldPosition.x) && (x.y == worldPosition.y));
        }

        public Vector3 GetWorldPointPosition(Vector3 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }

        public bool IsTileAvailable(Vector2 worldPositionValue)
        {
            return Instance.AvailableLocations.Keys.Any(position => (position.x == worldPositionValue.x && position.y == worldPositionValue.y));
        }

        public void SetTileActive(Vector3Int cellPosition)
        {
            Instance.HighlighTilemap.SetTile(cellPosition, OverlayTile);
        }

        public void SetTileOverlayed(Vector3Int cellPosition)
        {
            if (cellPosition != PreviousCellPosition)
            {
                Instance.HighlighTilemap.SetTile(PreviousCellPosition, null);
                Instance.HighlighTilemap.SetTile(cellPosition, OverlayTile);
                Instance.PreviousCellPosition = cellPosition;
            }
        }

        public void SetTileStandard(Vector3Int cellPosition)
        {
            Tilemap.SetTileFlags(cellPosition, TileFlags.None);
            Tilemap.SetColor(cellPosition, Color.white);
        }

        public Vector3Int MouseToCell(Vector2 mouseInput)
        {
            Vector3 worldPos = MouseToWorld(mouseInput);
            Vector3Int cellPos = WorldToCell(worldPos);
            return cellPos;
        }

        public Vector3 MouseToWorld(Vector2 mouseInput)
        {
            return Camera.main.ScreenToWorldPoint(mouseInput);
        }

        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return Instance.Grid.WorldToCell(worldPosition);
        }

        public Vector3 CellCenterToWorld(Vector3Int cellPosition)
        {
            return Instance.Tilemap.GetCellCenterWorld(cellPosition);
        }

        public bool IsCellInMap(Vector3Int cellPosition)
        {
            return Instance.Tilemap.HasTile(cellPosition);
        }

        public Vector3Int GetRandomCellPosition()
        {
            int x = Instance.GetRandomXInBounds();
            int y = Instance.GetRandomYInBounds();
            return new Vector3Int(x, y);
        }

        public Vector3Int GetRandomValidCellPosition()
        {
            var cellPosition = Instance.GetRandomCellPosition();
            while (!Instance.Tilemap.HasTile(cellPosition))
            {
                cellPosition = Instance.GetRandomCellPosition();
            }
            return cellPosition;
        }

        private int GetRandomYInBounds()
        {
            var yMin = Instance.Tilemap.cellBounds.yMin;
            var yMax = Instance.Tilemap.cellBounds.yMax;
            var y = Random.Range(yMin, yMax);
            return y;
        }

        private int GetRandomXInBounds()
        {
            var xMin = Instance.Tilemap.cellBounds.xMin;
            var xMax = Instance.Tilemap.cellBounds.xMax;
            var x = Random.Range(xMin, xMax);
            return x;
        }

        private void Awake()
        {
            Instance.AvailableLocations = new Dictionary<Vector3Int, Vector3>();
        }

        private void Start()
        {
            if (Instance.Tilemap == null)
            {
                Instance.Tilemap = GetComponent<Tilemap>();
            }

            Instance.FillAvailableTiles();
        }

        private void FillAvailableTiles()
        {
            foreach (var pos in Instance.Tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = Instance.Tilemap.CellToWorld(localPlace);
                if (Instance.Tilemap.HasTile(localPlace))
                {
                    Instance.AvailableLocations.Add(localPlace, place);
                }
            }
        }
    }
}