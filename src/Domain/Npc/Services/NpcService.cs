namespace Assets.Scripts.src.Domain.Npc.Services
{
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Domain.Map.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class NpcService : Singleton<NpcService>
    {
        public GameObject NpcContainer;

        public GameObject NpcPrefab;

        private ArrayList _npcs = new ArrayList();

        public bool IsNpcInstantiated
        { get { return _npcs.Count > 0; } }

        public void InstantiateNpc()
        {
            Debug.Log("Instantiate Npc");
            StartCoroutine(InstantiateNpcCoroutine());
        }

        private IEnumerator InstantiateNpcCoroutine()
        {
            Time.timeScale = 0;
            while (true)
            {
                Vector3Int cellPosition = MapService.Instance.GetRandomValidCellPosition();
                if (IsCellInMap(cellPosition) && !IsNpcInstantiated)
                {
                    InstantiateNpcAtCell(cellPosition);
                    
                    yield break;
                }
                yield return null;
            }
        }

        private static bool IsCellInMap(Vector3Int cellPosition)
        {
            return MapService.Instance.Tilemap.HasTile(cellPosition);
        }      

        private void InstantiateNpcAtCell(Vector3Int cellPosition)
        {
            _npcs.Add(Instantiate(NpcPrefab, MapService.Instance.Tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity, NpcContainer.transform));
            Time.timeScale = 1;
            GameStateService.Instance.SetINSTANTIATED_NPCState();
        }
    }
}