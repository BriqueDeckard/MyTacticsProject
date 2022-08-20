namespace Assets.Scripts.src.Domain.Npc.Services
{
    using Assets.Scripts.src.Domain.Npc.Domain;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Domain.Map.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.Text.Services;

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
            var npcGameObject = Instantiate(NpcPrefab, MapService.Instance.Tilemap.GetCellCenterWorld(cellPosition), Quaternion.identity, NpcContainer.transform);
            var npcScript = ProcessNpcGameObject(cellPosition, npcGameObject);
            ProcessNpcScript(cellPosition, npcScript);

            StartCoroutine(WaitForClick());
        }

        private Npc ProcessNpcGameObject(Vector3Int cellPosition, GameObject npcGameObject)
        {
            _npcs.Add(npcGameObject);

            return npcGameObject.GetComponent<Npc>();            
        }

        private static void ProcessNpcScript(Vector3Int cellPosition, Npc npcScript)
        {
            if (npcScript != null)
            {
                MapService.Instance.OccupiedLocations.Add(cellPosition, npcScript);
                GameStateService.Instance.SetINSTANTIATED_NPCState();
                Time.timeScale = 1;
            }
            else
            {
                throw new System.Exception("NpcScript is null.");
            }
        }

        private IEnumerator WaitForClick()
        {
            Time.timeScale = 0;
            while (true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame){
                    Time.timeScale = 1;
                    yield break;
                }                
                yield return null;
            }
        }

        public void ProcessNpcUnit(GameObject unit)
        {
            Npc npc = unit.GetComponent<Npc>();
            Debug.Log("Unit: " + unit.name + " - " + npc.TeamTag);
            npc.CharacterAction();
        }

        public void InstantiateNpcInfo()
        {
            GameStateService.Instance.SetINSTANTIATING_NPCState();
            Debug.Log("Instantiate team NPC");
            PanelService.Instance.TogglePanel(Panels.PANEL_1, true);
            TextService.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "NPC is instantiating.");
        }
    }
}