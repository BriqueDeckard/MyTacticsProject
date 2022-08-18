using Assets.Scripts.src.Common.Contracts;
using System;
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

        private void Awake()
        {
            AvailableLocations = new Dictionary<Vector3Int, Vector3>();
        }

        private void Start()
        {
            if (Tilemap == null)
            {
                Tilemap = GetComponent<Tilemap>();
            }

            FillAvailableTiles();
        }

        private void FillAvailableTiles()
        {
            foreach (var pos in Tilemap.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
                Vector3 place = Tilemap.CellToWorld(localPlace);
                if (Tilemap.HasTile(localPlace))
                {
                    AvailableLocations.Add(localPlace, place);
                }
            }
        }

        public Vector3Int GetCellPosition(Vector3 vector3)
        {
            return Grid.WorldToCell(vector3);
        }

  

        public bool TileIsAvailable(Vector3Int position)
        {
            var worldPosition = Grid.CellToWorld(position);
            return AvailableLocations.Keys.Any(x => (x.x == worldPosition.x) && (x.y == worldPosition.y));
        }

        public Vector3 GetWorldPointPosition(Vector3 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }

        public bool IsTileAvailable(Vector2 worldPositionValue)
        {
            return AvailableLocations.Keys.Any(position => (position.x == worldPositionValue.x && position.y == worldPositionValue.y));
        }

        public void SetTileActive(Vector3Int cellPosition)
        {
            HighlighTilemap.SetTile(cellPosition, OverlayTile);
        }
        public void SetTileOverlayed(Vector3Int cellPosition)
        {
            if(cellPosition != PreviousCellPosition)
            {
                HighlighTilemap.SetTile(PreviousCellPosition, null);
                HighlighTilemap.SetTile(cellPosition, OverlayTile);
                PreviousCellPosition = cellPosition;
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
            return Grid.WorldToCell(worldPosition);
        }

        public Vector3 CellCenterToWorld(Vector3Int cellPosition)
        {
            return Tilemap.GetCellCenterWorld(cellPosition);
        }

        public bool HasTile(Vector3Int tilePosition)
        {
            var hasTile = Tilemap.HasTile(tilePosition);
            return hasTile;
        }
    }
}