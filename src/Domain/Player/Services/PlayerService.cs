namespace Assets.Scripts.src.Domain.Player.Services
{
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Map.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerService : Singleton<PlayerService>
    {
        private ArrayList _players = new ArrayList();

        public GameObject PlayerContainer;

        public GameObject PlayerPrefab;

        public bool IsPlayerInstantiated
        { get { return _players.Count > 0; } }

        public void InstantiatePlayer()
        {
            Debug.Log("Instantiate player");
            StartCoroutine(WaitForClick());
            return;
        }

        private IEnumerator WaitForClick()
        {
            Time.timeScale = 0;
            while (true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Vector3Int cellPosition = GetCellFromMousePosition();

                    if (MapService.Instance.IsCellInMap(cellPosition) && !IsPlayerInstantiated)
                    {
                        InstantiatePlayerAtCell(cellPosition);
                        yield break;
                    }
                    else
                    {
                        Debug.Log("Position: " + cellPosition + " is not in tilemap cell bounds");
                    }
                }
                yield return null;
            }
        }

        private void InstantiatePlayerAtCell(Vector3Int cellPosition)
        {
            _players.Add(Instantiate(PlayerPrefab, MapService.Instance.Tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity, PlayerContainer.transform));
            GameStateService.Instance.SetINSTANTIATED_PLAYERState();
            Time.timeScale = 1;
        }

        private static Vector3Int GetCellFromMousePosition()
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

        private void Awake()
        {
            _players = new ArrayList();
        }
    }
}