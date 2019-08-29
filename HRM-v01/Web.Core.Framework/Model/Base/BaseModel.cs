using System.Linq;
using System.Reflection;

namespace Web.Core.Framework
{
    public class BaseModel<T> where T : class, new()
    {
        public BaseModel()
        {
            Init(new T());
        }

        public BaseModel(T entity)
        {
            entity = entity ?? new T();

            Init(entity);
        }

        // base properties
        public int Id { get; set; }

        /// <summary>
        /// init model from entity
        /// </summary>
        /// <param name="entity"></param>
        protected void Init(T entity)
        {
            // init type
            var modelType = GetType();
            var entityType = typeof(T);
            // init props array
            var modelProps = modelType.GetProperties().Select(p => p.Name).ToList();
            var entityProps = typeof(T).GetProperties().Select(p => p.Name).ToList();
            // set properties
            foreach (var prop in modelProps)
            {
                if (entityProps.Contains(prop))
                {
                    // get prop value
                    var propValue = entityType.InvokeMember(prop, BindingFlags.GetProperty, null, entity, null);
                    // set model prop
                    modelType.InvokeMember(prop, BindingFlags.SetProperty, null, this, new[] {propValue});
                }
            }
        }

        /// <summary>
        /// fill model props to entity
        /// </summary>
        /// <param name="entity"></param>
        public void FillEntity(ref T entity)
        {
            // init type
            var modelType = GetType();
            var entityType = entity.GetType();
            // init props array
            var modelProps = modelType.GetProperties().Select(p => p.Name).ToList();
            var entityProps = typeof(T).GetProperties().Select(p => p.Name).ToList();
            // set properties
            foreach (var prop in entityProps)
            {
                if (modelProps.Contains(prop))
                {
                    // get prop value
                    var propValue = modelType.InvokeMember(prop, BindingFlags.GetProperty, null, this, null);
                    // set entity prop
                    entityType.InvokeMember(prop, BindingFlags.SetProperty, null, entity, new[] { propValue });
                }
            }
        }
    }
}
