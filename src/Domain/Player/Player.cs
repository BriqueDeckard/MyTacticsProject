namespace Assets.Scripts.src.Player
{
    using Character.Domain;
    using TeamTag.Domain.Enum;

    public class Player : Character
    {
        public Player()
        {
            this.Team = TeamTag.PLAYER;
        }
    }
}