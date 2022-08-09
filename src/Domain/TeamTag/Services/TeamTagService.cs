namespace Assets.Scripts.src.TeamTag.Services
{
    using Common.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TeamTag.Domain.Enum;
    using UnityEngine;

    /// <summary>
    /// Manages operations related to TeamTags
    /// </summary>
    public class TeamTagService : Singleton<TeamTagService>
    {
        #region UnityLifeCycle

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            Debug.Log("" + name + "." + GetType().Name + " has started.");
        }

        #endregion UnityLifeCycle

        /// <summary>
        /// Teamtag list to keep track of the running order.
        /// </summary>
        public List<TeamTag> TeamTagList => Enum.GetValues(typeof(TeamTag))
            .Cast<TeamTag>()
            .ToList();

        /// <summary>
        /// Gets a random TeamTag.
        /// </summary>
        public TeamTag RandomTeamTag
        {
            get
            {
                var index = UnityEngine.Random.Range(0, TeamTagList.Count);
                return TeamTagList[index];
            }
        }

        /// <summary>
        /// Gets all the TeamTags except the one passed in parameter.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public List<TeamTag> OtherTeamTags(TeamTag current)
        {
            return TeamTagList
                .FindAll(x => x != current)
                .ToList();
        }
    }
}