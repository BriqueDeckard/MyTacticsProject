namespace Assets.Scripts.src.Application.Panel.Services
{
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class PanelMouseService : Singleton<PanelMouseService>
    {
        public Button InfoButton1;

        public UnityEvent CharactersServiceEvent;

        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}