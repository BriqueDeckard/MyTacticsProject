namespace Assets.Scripts.src.Domain.Character.Services
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Application.TeamTagQueue.Services;
    using Assets.Scripts.src.Application.Text.Services;
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

        public void InstantiateCharacters(TeamTag teamTag)
        {
            Debug.Log("For team : " + teamTag + ",  0 units, need to instantiate");
            Time.timeScale = 0;
            if (teamTag.Equals(TeamTag.PLAYER))
            {
                PlayerService.Instance.InstantiatePlayerInfo();
            }
            else if (teamTag.Equals(TeamTag.NPC))
            {
                NpcService.Instance.InstantiateNpcInfo();
            }
        }

       

       
    }
}