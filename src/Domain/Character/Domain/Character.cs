namespace Assets.Scripts.src.Character.Domain
{
    using Assets.Scripts.src.TeamTag.Domain.Enum;
    using UnityEngine;

    public abstract class Character : MonoBehaviour
    {
        protected TeamTag Team;

        public TeamTag TeamTag => Team;

        public abstract void CharacterAction();
    }
}