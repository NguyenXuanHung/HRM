using Web.Core.Object.Kpi;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for EvaluationCalculateModel
    /// </summary>
    public class EvaluationCalculateModel : BaseModel<kpi_Evaluation>
    {
        public EvaluationCalculateModel(kpi_Evaluation evaluation)
        {
            //set props
            Id = evaluation.Id;
        }
        
    }
}
