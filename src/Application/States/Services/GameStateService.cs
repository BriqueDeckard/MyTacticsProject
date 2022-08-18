namespace Assets.Scripts.src.Application.States.Services
{
    using Assets.Scripts.src.Application.States.Domain.Enum;
    using Assets.Scripts.src.Common.Contracts;
    using System;
    using System.Collections.Generic;

    public class GameStateService : Singleton<GameStateService>
    {
        public List<GameState> history;

        public GameState currentGameState = GameState.DEFAULT;

        public GameState previousGameState = GameState.DEFAULT;

        private void SetGameState(GameState gameState)
        {
            previousGameState = currentGameState;
            currentGameState = gameState;
            history.Add(gameState);
        }

        public void SetINSTANTIATING_PLAYERState()
        {
            SetGameState(GameState.INSTANTIATING_PLAYER);
        }

        public void SetINSTANTIATED_PLAYERState()
        {
            SetGameState(GameState.INSTANTIATED_PLAYER);
        }

        public void SetINSTANTIATING_NPCState()
        {
            SetGameState(GameState.INSTANTIATING_NPC);
        }

        public void SetINSTANTIATED_NPCState()
        {
            SetGameState(GameState.INSTANTIATED_NPC);
        }

        public void SetGAME_IS_READYState()
        {
            SetGameState(GameState.GAME_IS_READY);
        }

        private void Awake()
        {
            history = new List<GameState>();
        }

       
    }
}