namespace Assets.Scripts.src.Application.Mouse.Services
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Domain.Enum;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Domain.Map.Services;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class MouseService : Singleton<MouseService>
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if (
                (!PanelService.Instance.Panel1IsActive)
                && 
                (!GameStateService.Instance.currentGameState.Equals(GameState.INSTANTIATING_NPC))
                &&
                (!GameStateService.Instance.currentGameState.Equals(GameState.INSTANTIATED_NPC))
                )
            {
                if (Mouse.current.wasUpdatedThisFrame)
                {
                    Mouse mouse = Mouse.current;
                    Vector2 mousePositionValue = mouse.position.ReadValue();
                    Vector2 worldPositionValue = Camera.main.ScreenToWorldPoint(mousePositionValue);

                    Vector3Int cellPosition = MapService.Instance.Grid.WorldToCell(worldPositionValue);
                    bool isTileInMap = MapService.Instance.Tilemap.HasTile(cellPosition);
                    if (isTileInMap)
                    {
                        MapService.Instance.SetTileOverlayed(cellPosition);
                    }
                }
            }
            
        }

        public Vector3Int GetCellFromMousePosition()
        {
            //         Get mouse position
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Debug.Log("Mouse position: " + mousePosition);
            var worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPoint.z = 0;
            Debug.Log("World point: " + worldPoint);
            var cellPosition = MapService.Instance.Tilemap.WorldToCell(worldPoint);
            return cellPosition;
        }
    }
}