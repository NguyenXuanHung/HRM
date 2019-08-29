namespace Web.Core.Framework.Report
{
    public class FilterCondition
    {
        public string Name { get; set; }
        public string Clause { get; set; }
        
        /// <summary>
        /// Constructor with init value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="clause"></param>
        public FilterCondition(string name, string clause)
        {
            Name = name;
            Clause = clause;
        }
    }
}
