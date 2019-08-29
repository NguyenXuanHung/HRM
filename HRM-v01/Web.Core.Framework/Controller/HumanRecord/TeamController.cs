using System.Collections.Generic;
using System.Linq;
using Web.Core.Framework.Model;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework.Controller
{
    public class TeamController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TeamModel GetById(int id)
        {
            var recordTeam = hr_TeamServices.GetById(id);
            return new TeamModel(recordTeam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static TeamModel GetByRecordId(int recordId)
        {
            var record = hr_TeamServices.GetByRecordId(recordId);
            return new TeamModel(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<TeamModel> GetAll(int? recordId)
        {
            var goAboards = hr_TeamServices.GetAll(recordId);
            return goAboards.Select(goAboard => new TeamModel(goAboard)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Update(hr_Team obj)
        {
            var record = hr_TeamServices.GetById(obj.Id);
            if(record == null) return;
            record.RecordId = obj.RecordId;
            record.CreatedBy = obj.CreatedBy;
            record.CreatedDate = obj.CreatedDate;
            record.EditedDate = obj.EditedDate;
            record.TeamId = obj.TeamId;
            record.ConstructionId = obj.ConstructionId;
            record.WorkingFormId = obj.WorkingFormId;
            record.ProbationWorkingTime = obj.ProbationWorkingTime;
            record.StudyWorkingDay = obj.StudyWorkingDay;
            record.WorkLocationId = obj.WorkLocationId;
            record.GraduationYear = obj.GraduationYear;
            record.UnionJoinedDate = obj.UnionJoinedDate;
            record.UnionJoinedPlace = obj.UnionJoinedPlace;
            record.UnionJoinedPosition = obj.UnionJoinedPosition;
            record.GraduationTypeId = obj.GraduationTypeId;
            record.UniversityId = obj.UniversityId;
            hr_TeamServices.Update(record);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="team"></param>
        public void Insert(hr_Team team)
        {
            hr_TeamServices.Create(team);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            hr_TeamServices.Delete(id);
        }
    }
}
