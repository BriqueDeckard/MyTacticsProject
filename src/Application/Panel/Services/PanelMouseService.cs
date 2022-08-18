namespace Assets.Scripts.src.Application.Panel.Services
{
    using Assets.Scripts.src.Application.Text.Services;
    using Assets.Scripts.src.Common.Contracts;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;

    public class PanelMouseService : Singleton<PanelMouseService>
    {
        public Button InfoButton1;

        public UnityEvent CharactersServiceEvent;

        // Start is called before the first frame update
        private void Start()
        {
            StartWaitForButton();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void StartWaitForButton()
        {
            Time.timeScale = 0;
            InfoButton1.onClick.AddListener(ClickOnButton1());
        }

        private UnityAction ClickOnButton1()
        {
            return () =>
            {
                Debug.Log("Click on : Button 1");
                TextService.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "Position characters: ");
                PanelService.Instance.TogglePanel(Panels.PANEL_1, false);
                CharactersServiceEvent.Invoke();
            };
        }
    }
}