namespace Web.Core.Framework
{
    public class ListItemModel
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ListItemModel()
        {
            Key = "";
            Value = "";
        }

        public ListItemModel(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
