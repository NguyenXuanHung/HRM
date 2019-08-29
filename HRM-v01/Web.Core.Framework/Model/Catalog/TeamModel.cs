using Web.Core.Object.HumanRecord;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework.Model
{
    public class TeamModel
    {
        private readonly hr_Team _team;

        public TeamModel(hr_Team team)
        {
            _team = team ?? new hr_Team();

            RecordId = _team.RecordId;
            TeamId = _team.TeamId;
            ConstructionId = _team.ConstructionId;
            WorkLocationId = _team.WorkLocationId;
            WorkingFormId = _team.WorkingFormId;
            ProbationWorkingTime = _team.ProbationWorkingTime;
            StudyWorkingDay = _team.StudyWorkingDay;
        }

        /// <summary>
        /// 
        /// </summary>
        public int RecordId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TeamName
        {
            get { return cat_TeamServices.GetFieldValueById(TeamId); }
        }
        public int ConstructionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public  string ConstructionName { get { return cat_ConstructionServices.GetFieldValueById(ConstructionId); } }

        /// <summary>
        /// 
        /// </summary>
        public  int WorkingFormId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProbationWorkingTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int StudyWorkingDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WorkLocationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WorkLocationName
        {
            get { return cat_WorkLocationServices.GetFieldValueById(WorkLocationId); }
        }

    }
}
