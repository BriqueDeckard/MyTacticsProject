namespace Assets.Scripts.src.Npc.Domain
{
    using Character.Domain;
    using TeamTag.Domain.Enum;

    public class Npc : Character
    {
        public Npc()
        {
            Team = TeamTag.NPC;
        }
    }
}