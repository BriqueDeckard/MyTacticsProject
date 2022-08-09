namespace Assets.Scripts.src.Character.Services
{
    using Assets.Scripts.src.Common.Contracts;
    using UnityEngine;

    public class CharacterService : Singleton<CharacterService>
    {
        public void ToInvoke()
        {
            Debug.Log("Invoke CharacterService");
        }
    }
}