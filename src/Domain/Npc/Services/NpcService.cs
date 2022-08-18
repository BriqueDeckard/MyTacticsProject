namespace Assets.Scripts.src.Domain.Npc.Services
{
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Domain.Map.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.InputSystem.Controls;

    public class NpcService : Singleton<NpcService>
    {
        public GameObject NpcContainer;

        public GameObject NpcPrefab;

        private ArrayList _npcs = new ArrayList();
        public bool IsNpcInstantiated { get { return _npcs.Count > 0; } }

        public void InstantiateNpc()
        {
            Debug.Log("Instantiate Npc");
            StartCoroutine(WaitForClick());
        }

        private IEnumerator WaitForClick()
        {
            Time.timeScale = 0;
            while (true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Vector3Int cellPosition = GetCellFromMousePosition();

                    if (IsCellInMap(cellPosition) && !IsNpcInstantiated)
                    {
                        InstantiateNpcAtCell(cellPosition);
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
        private static bool IsCellInMap(Vector3Int cellPosition)
        {
            return MapService.Instance.Tilemap.HasTile(cellPosition);
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

        private void InstantiateNpcAtCell(Vector3Int cellPosition)
        {
            _npcs.Add(Instantiate(NpcPrefab, MapService.Instance.Tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity, NpcContainer.transform));
            Time.timeScale = 1;
            GameStateService.Instance.SetINSTANTIATED_NPCState();
        }
    }
}