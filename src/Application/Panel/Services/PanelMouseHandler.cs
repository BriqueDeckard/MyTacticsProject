namespace Assets.Scripts.src.Panel.Services
{
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Text.Services;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;

    public class PanelMouseHandler : Singleton<PanelMouseHandler>
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
                TextManager.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "Position characters: ");
                PanelManager.Instance.TogglePanel(Panels.PANEL_1, false);
                CharactersServiceEvent.Invoke();
            };
        }

        public void StartWaitForClick()
        {
            Time.timeScale = 0;
            StartCoroutine(WaitForClick());
        }

        private IEnumerator WaitForClick()
        {
            while (true)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("MOUSE");
                    Time.timeScale = 1;
                    yield break;
                }
                yield return null;
            }
        }
    }
}