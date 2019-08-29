namespace Web.Core.Framework
{
    public interface IBaseControl
    {
        string GetID();
        object GetValue();
        void SetValue(object value);
    }
}
