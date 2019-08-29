namespace Web.Core.Framework
{
    public class PermissionModel
    {
        /// <summary>
        /// Constructor without permission
        /// </summary>
        public PermissionModel()
        {
            CanRead = false;
            CanWrite = false;
            CanDelete = false;
            HasFullControl = false;
        }

        /// <summary>
        /// Constructor with permission description
        /// </summary>
        /// <param name="canRead"></param>
        /// <param name="canWrite"></param>
        /// <param name="canDelete"></param>
        /// <param name="fullControl"></param>
        public PermissionModel(bool canRead, bool canWrite, bool canDelete, bool fullControl)
        {
            CanRead = canRead;
            CanWrite = canWrite;
            CanDelete = canDelete;
            HasFullControl = fullControl;
        }
        
        /// <summary>
        /// has read permission
        /// </summary>
        public bool CanRead { get; set; }

        // has write permission
        public bool CanWrite { get; set; }

        // has delete permission
        public bool CanDelete { get; set; }

        // has full control permission
        public bool HasFullControl { get; set; }
    }
}
