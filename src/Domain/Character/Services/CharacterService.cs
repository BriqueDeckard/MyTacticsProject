namespace Assets.Scripts.src.Character.Services
{
    using Assets.Scripts.src.Application.TeamTagQueue.Services;
    using Assets.Scripts.src.Common.Contracts;
    using Assets.Scripts.src.Domain.Npc.Services;
    using Assets.Scripts.src.Domain.Player.Services;
    using Assets.Scripts.src.TeamTag.Domain.Enum;
    using UnityEngine;

    public class CharacterService : Singleton<CharacterService>
    {
        public void ToInvoke()
        {
            Debug.Log("Invoke CharacterService");
            var currentTeam = TeamTagQueueService.Instance.CurrentTeamTag;
            switch (currentTeam)
            {
                case TeamTag.PLAYER:
                    PlayerService.Instance.InstantiatePlayer();
                    break;

                case TeamTag.NPC:
                    NpcService.Instance.InstantiateNpc();
                    break;

                default:
                    Debug.Log("Doh");
                    break;
            }
        }
    }
}