namespace Assets.Scripts.src.Domain.Player.Domain
{
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
            Debug.Log("Character Action");
        }
    }
}