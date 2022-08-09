namespace Assets.Scripts.src.TeamTagQueue.Services
{
    using Assets.Scripts.src.Panel.Services;
    using Assets.Scripts.src.Text.Services;
    using Common.Contracts;
    using System.Collections.Generic;
    using TeamTag.Domain.Enum;
    using TeamTag.Services;
    using UnityEngine;

    /// <summary>
    /// Manages operations related to the TeamTag queue.
    /// </summary>
    public class TeamTagQueueService : Singleton<TeamTagQueueService>
    {
        /// <summary>
        /// Is TeamTagQueue empty?
        /// </summary>
        public bool TeamTagQueueIsEmpty { get => teamTagQueue.Count <= 0; }

        /// <summary>
        /// The TeamTagQueue
        /// </summary>
        private Queue<TeamTag> teamTagQueue;

        /// <summary>
        /// Keeps track of the order of the TeamTags in the queue.
        /// </summary>
        private List<TeamTag> teamTagOrder;

        #region UnityLifeCycle

        private void Awake()
        {
            teamTagQueue = new Queue<TeamTag>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("" + this.name + "." + this.GetType().Name + " has started.");
        }

        private void Update()
        {
        }

        #endregion UnityLifeCycle

        /// <summary>
        /// Fill the teamstag queue
        /// </summary>
        public void FillTeamTagQueue()
        {
            // If the teamtag order is null, then it must be initialized and filled.
            if (teamTagOrder == null)
            {
                FillTeamTagOrder();
            }

            // Otherwise, the teams are queued in the given order.
            teamTagOrder.ForEach(x => teamTagQueue.Enqueue(x));
        }

        /// <summary>
        /// Order the teams for the rest of the game.
        /// </summary>
        private void FillTeamTagOrder()
        {
            // Initialize the list with the one that starts positioned first.
            teamTagOrder = new List<TeamTag>();
            TeamTag beginner = TeamTagService.Instance.RandomTeamTag;
            teamTagOrder.Add(beginner);

            // Add the other teams.
            List<TeamTag> others = TeamTagService.Instance.OtherTeamTags(beginner);
            teamTagOrder.AddRange(others);
        }

        /// <summary>
        /// Dequeue the TeamTag queue.
        /// </summary>
        public void DequeueTeamTagQueue()
        {
            // Get the teamtag for this turn and the units corresponding to this team.
            var teamTag = teamTagQueue.Dequeue();
            var teamUnits = GameObject.FindGameObjectsWithTag(teamTag.ToString());
            if (teamUnits.Length <= 0)
            {
                Debug.Log("For team : " + teamTag + ",  0 units, need to instantiate");
                Time.timeScale = 0;
                if (teamTag.Equals(TeamTag.PLAYER))
                {
                    Debug.Log("Instantiate team PLAYER");
                    PanelManager.Instance.TogglePanel(Panels.PANEL_1, true);
                    TextManager.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "PLAYER plays.");
                }
                else if (teamTag.Equals(TeamTag.NPC))
                {
                    Debug.Log("Instantiate team NPC");
                    PanelManager.Instance.TogglePanel(Panels.PANEL_1, true);
                    TextManager.Instance.SetInfoTextText(InfoText.INFO_TEXT_1, "NPC plays.");
                }
            }
        }
    }
}