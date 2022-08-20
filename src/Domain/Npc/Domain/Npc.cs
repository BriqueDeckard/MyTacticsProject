namespace Assets.Scripts.src.Domain.Npc.Domain
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Application.Text.Services;
    using Character.Domain;
    using TeamTag.Domain.Enum;
    using UnityEngine;

    public class Npc : Character
    {
        public Npc()
        {
            Team = TeamTag.NPC;
        }

        public override void CharacterAction()
        {
            Time.timeScale = 0;
            PanelService.Instance.TogglePanel(Panels.PANEL_1, true);
            TextService.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "NPC is playing");
            GameStateService.Instance.SetNPC_IS_PLAYINGState();
            // TODO: Npc action
            GameStateService.Instance.SetNPC_HAS_PLAYEDState();
        }
    }
}