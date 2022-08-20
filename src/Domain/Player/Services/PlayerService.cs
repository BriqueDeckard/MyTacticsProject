namespace Assets.Scripts.src.Domain.Player.Services
{
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Map.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Assets.Scripts.src.Domain.Player.Domain;
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.Text.Services;

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

        public void ProcessPlayerUnit(GameObject unit)
        {
            MyPlayer myPlayer = unit.GetComponent<MyPlayer>();
            Debug.Log("Unit: " + unit.name + " - " + myPlayer.TeamTag);
            myPlayer.CharacterAction();
        }

        public void InstantiatePlayerInfo()
        {
            GameStateService.Instance.SetINSTANTIATING_PLAYERState();
            Debug.Log("Instantiate team PLAYER");
            PanelService.Instance.TogglePanel(Panels.PANEL_1, true);
            TextService.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "Please instantiate PLAYER.");
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
            var playerGO = Instantiate(PlayerPrefab, MapService.Instance.Tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity, PlayerContainer.transform);

            _players.Add(playerGO);
            MyPlayer playerScript = playerGO.GetComponent<MyPlayer>();
            if (playerScript != null)
            {
                MapService.Instance.OccupiedLocations.Add(cellPosition, playerScript);
                GameStateService.Instance.SetINSTANTIATED_PLAYERState();
                Time.timeScale = 1;
            }
            else
            {
                throw new System.Exception("PlayerScript is null");
            }            
        }

        private Vector3Int GetCellFromMousePosition()
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