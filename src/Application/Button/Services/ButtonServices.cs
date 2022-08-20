namespace Assets.Scripts.src.Application.Button.Services
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Common.Contracts;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ButtonServices : Singleton<ButtonServices>
    {
        public Button InfoButton1;
        public UnityEvent CharactersServiceEvent;

        public void ClickOnButton1()
        {
            var currentState = GameStateService.Instance.currentGameState;

            switch (currentState)
            {
                case States.Domain.Enum.GameState.INSTANTIATING_PLAYER:
                    CharactersServiceEvent.Invoke();
                    PanelService.Instance.TogglePanel(Panels.PANEL_1, false);
                    break;

                case States.Domain.Enum.GameState.INSTANTIATING_NPC:
                    CharactersServiceEvent.Invoke();
                    PanelService.Instance.TogglePanel(Panels.PANEL_1, false);
                    break;
                default:
                    Time.timeScale = 1;
                    break;
            }
        }
    }
}