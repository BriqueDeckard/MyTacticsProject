namespace Assets.Scripts.src.Application.States.Services
{
    using Assets.Scripts.src.Application.States.Domain.Enum;
    using Assets.Scripts.src.Common.Contracts;
    using System.Collections.Generic;
    using static Assets.Scripts.src.Application.States.Domain.Enum.GameState;

    public class GameStateService : Singleton<GameStateService>
    {
        public List<GameState> history;

        public GameState currentGameState = DEFAULT;

        public GameState previousGameState = DEFAULT;

        private void SetGameState(GameState gameState)
        {
            previousGameState = currentGameState;
            currentGameState = gameState;
            history.Add(gameState);
        }

        public void SetINSTANTIATING_PLAYERState()
        {
            SetGameState(INSTANTIATING_PLAYER);
        }

        public void SetINSTANTIATED_PLAYERState()
        {
            SetGameState(INSTANTIATED_PLAYER);
        }

        public void SetINSTANTIATING_NPCState()
        {
            SetGameState(INSTANTIATING_NPC);
        }

        public void SetINSTANTIATED_NPCState()
        {
            SetGameState(INSTANTIATED_NPC);
        }

        public void SetGAME_IS_READYState()
        {
            SetGameState(GAME_IS_READY);
        }

        public void SetPLAYER_IS_PLAYINGState()
        {
            SetGameState(PLAYER_IS_PLAYING);
        }

        public void SetNPC_HAS_PLAYEDState()
        {
            SetGameState(NPC_HAS_PLAYED);
        }

        public void SetPLAYER_HAS_PLAYEDState()
        {
            SetGameState(PLAYER_HAS_PLAYED);
        }

        public void SetNPC_IS_PLAYINGState()
        {
            SetGameState(NPC_IS_PLAYING);
        }

        private void Awake()
        {
            history = new List<GameState>();
        }
    }
}