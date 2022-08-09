namespace archive.Turn.Services
{
    using Assets.Scripts.src.Common.Contracts;
    using UnityEngine;
    using UnityEngine.Events;

    public class OldTurnManager : Singleton<OldTurnManager>
    {
        public bool ReadyToPlay=false;
        public UnityEvent InstantiatePlayer;

        private void Update()
        {
            if (!ReadyToPlay)
            {
                InstantiatePlayer.Invoke();
            }
            else
            {
                Debug.Log("READY");
            }
        }
    }
}