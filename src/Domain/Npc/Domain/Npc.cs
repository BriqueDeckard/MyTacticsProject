namespace Assets.Scripts.src.Domain.Npc.Domain.Npc
{

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
            Debug.Log("NPC Action");
        }
    }
}