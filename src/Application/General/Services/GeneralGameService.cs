namespace Assets.Scripts.src.Common.Contracts
{
    using System;
    using TeamTagQueue.Services;
    using UnityEngine;

    /// <summary>
    /// Takes care of the general workflow of the game.
    /// </summary>
    public class GeneralGameService : Singleton<GeneralGameService>
    {
        #region UnityLifeCycle

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            Debug.Log("" + this.name + "." + this.GetType().Name + " has started.");
        }

        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
        }

        /// <summary>
        ///
        /// </summary>
        private void FixedUpdate()
        {
            Lifecycle();
        }

        #endregion UnityLifeCycle

        /// <summary>
        /// general workflow of the game
        /// </summary>
        private void Lifecycle()
        {
            FillTeamTagQueueIfEmpty(() => TeamTagQueueService.Instance.TeamTagQueueIsEmpty);
            DequeueTeamTagQueueIfNotEmpty(() => TeamTagQueueService.Instance.TeamTagQueueIsEmpty);
        }

        /// <summary>
        /// Fills the teamTag queue if it is empty.
        /// </summary>
        /// <param name="teamTagQueueIsEmpty"></param>
        private void FillTeamTagQueueIfEmpty(Func<bool> teamTagQueueIsEmpty)
        {
            bool test = !teamTagQueueIsEmpty();
            if (test)
            {
                return;
            }

            TeamTagQueueService.Instance.FillTeamTagQueue();
            return;
        }

        /// <summary>
        /// Unfold the teamTag tail if it contains something.
        /// </summary>
        /// <param name="teamTagQueueIsEmpty"></param>
        private void DequeueTeamTagQueueIfNotEmpty(Func<bool> teamTagQueueIsEmpty)
        {
            bool test = teamTagQueueIsEmpty();
            if (test)
            {
                return;
            }

            while (!teamTagQueueIsEmpty())
            {
                TeamTagQueueService.Instance.DequeueTeamTagQueue();
                break;
            }
        }
    }
}