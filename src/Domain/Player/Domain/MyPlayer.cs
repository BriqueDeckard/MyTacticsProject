namespace Assets.Scripts.src.Domain.Player.Domain
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Application.Text.Services;
    using Character.Domain;
    using TeamTag.Domain.Enum;
    using UnityEngine;

    public class MyPlayer : Character
    {
        public MyPlayer()
        {
            this.Team = TeamTag.PLAYER;
        }

        public override void CharacterAction()
        {
            Time.timeScale = 0;
            GameStateService.Instance.SetPLAYER_IS_PLAYINGState();
            TextService.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "Player is playing");
            PanelService.Instance.TogglePanel(Panels.PANEL_1, true);
            // TODO: Player action
            GameStateService.Instance.SetPLAYER_HAS_PLAYEDState();

        }
    }
}