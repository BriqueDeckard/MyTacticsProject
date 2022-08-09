namespace archive.Player.Services
{
    using Assets.Scripts.src.Common.Contracts;
    using Map;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class OldPlayerManager : Singleton<OldPlayerManager>
    {
        public bool NeedToInstantiatePlayer = false;

        public GameObject PlayerPrefab;
        private List<GameObject> InstantiatedPlayers;
        public GameObject PlayerContainer;

        public UnityEvent GeneratePlayer;

        #region UnityLifecycle

        private void Awake()
        {
            InstantiatedPlayers = new List<GameObject>();
        }

        #endregion UnityLifecycle

        public void InstantiatePlayer()
        {
            if (NeedToInstantiatePlayer)
            {
                Debug.Log("Instantiate player");
                StartCoroutine(instantiatePlayer());
            }
        }

        private IEnumerator instantiatePlayer()
        {
            while (true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    var mousePosition = Mouse.current.position.ReadValue();
                    var worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    worldPosition.z = 0;
                    var cellPosition = OldMapManager.Instance.WorldToCell(worldPosition);
                    if (OldMapManager.Instance.WorldContainsCell(cellPosition))
                    {
                        worldPosition = OldMapManager.Instance.CellToWorld(cellPosition);
                        Debug.Log("WP: " + worldPosition);
                        InstantiatedPlayers.Add(Instantiate(
                            original: PlayerPrefab,
                            position: worldPosition,
                            rotation: Quaternion.identity,
                            parent: PlayerContainer.transform));
                        NeedToInstantiatePlayer = false;
                        GeneratePlayer.Invoke();
                        yield break;
                    }
                }
                yield return null;
            }
        }
    }
}