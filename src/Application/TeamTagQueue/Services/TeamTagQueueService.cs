namespace Assets.Scripts.src.Application.TeamTagQueue.Services
{
    using Assets.Scripts.src.Application.Panel.Services;
    using Assets.Scripts.src.Application.States.Services;
    using Assets.Scripts.src.Application.Text.Services;
    using Assets.Scripts.src.Domain.Character.Services;
    using Assets.Scripts.src.Domain.Npc.Domain;
    using Assets.Scripts.src.Domain.Npc.Services;
    using Assets.Scripts.src.Domain.Player.Domain;
    using Assets.Scripts.src.Domain.Player.Services;
    using Common.Contracts;
    using System.Collections.Generic;
    using System.Linq;
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

        private TeamTag currentTeamTag;

        public TeamTag CurrentTeamTag => currentTeamTag;

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
        /// Dequeue the TeamTag queue.
        /// </summary>
        public void DequeueTeamTagQueue()
        {
            // Get the teamtag for this turn and the units corresponding to this team.
            var teamTag = teamTagQueue.Dequeue();
            currentTeamTag = teamTag;
            var teamUnits = GameObject.FindGameObjectsWithTag(teamTag.ToString()).ToList();

            // If there is no characters, need to instantiate
            if (teamUnits.Count <= 0)
            {
                CharacterService.Instance.InstantiateCharacters(teamTag);
            }
            // Else, get team units, enqueue them and dequeue them
            else
            {
                ProcessTeamUnits(teamTag, teamUnits);
            }
        }


        /// <summary>
        /// Order the teams for the rest of the game.
        /// </summary>
        private void FillTeamTagOrder()
        {
            // Initialize the list with the one that starts positioned first.
            TeamTag beginner = InitializeTeamOrderList();
            // Add the other teams.
            List<TeamTag> others = TeamTagService.Instance.OtherTeamTags(beginner);
            teamTagOrder.AddRange(others);
        }

        private TeamTag InitializeTeamOrderList()
        {
            teamTagOrder = new List<TeamTag>();
            TeamTag beginner = TeamTagService.Instance.RandomTeamTag;
            teamTagOrder.Add(beginner);
            return beginner;
        }

        private static void ProcessTeamUnits(TeamTag teamTag, List<GameObject> teamUnits)
        {
            Debug.Log("Team " + teamTag + " plays.");
            var teamUnitsQueue = new Queue<GameObject>();
            teamUnits.ForEach(x => teamUnitsQueue.Enqueue(x));

            while (teamUnitsQueue.Count > 0)
            {
                var unit = teamUnitsQueue.Dequeue();
                ProcessCharacterUnit(teamTag, unit);
            }
        }

        private static void ProcessCharacterUnit(TeamTag teamTag, GameObject unit)
        {
            if (teamTag.Equals(TeamTag.PLAYER))
            {
                PlayerService.Instance.ProcessPlayerUnit(unit);
            }
            else
            {
                NpcService.Instance.ProcessNpcUnit(unit);
            }
        }
    }
}