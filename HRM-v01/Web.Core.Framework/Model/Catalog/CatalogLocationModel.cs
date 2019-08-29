using System;
using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class CatalogLocationModel : BaseModel<cat_Location>
    {
        private readonly cat_Location _location;
        private readonly cat_Location _parentLocation;

        public CatalogLocationModel()
        {
            // init object
            _location = new cat_Location();
            _parentLocation = new cat_Location();

            // set field
            Init(_location);
        }

        public CatalogLocationModel(cat_Location location)
        {
            // init object
            _location = location ?? new cat_Location();
            _parentLocation = cat_LocationServices.GetById(_location.ParentId);
            _parentLocation = _parentLocation ?? new cat_Location();

            // set field
            Init(_location);
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Parent Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Order
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Category status
        /// </summary>
        public CatalogStatus Status { get; set; }

        /// <summary>
        /// True if object was deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Created by
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Edited by
        /// </summary>
        public string EditedBy { get; set; }

        /// <summary>
        /// Edited date
        /// </summary>
        public DateTime EditedDate { get; set; }

        #region Custom props

        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName => !string.IsNullOrEmpty(_location.Group)
            ? ((CatalogGroupLocation)Enum.Parse(typeof(CatalogGroupLocation), _location.Group)).Description()
            : string.Empty;
        /// <summary>
        /// Root parent Id
        /// </summary>
        public int RootParentId => _parentLocation.ParentId;

        /// <summary>
        /// Parent name
        /// </summary>
        public string ParentName => _parentLocation.Name;

        /// <summary>
        /// Parent group
        /// </summary>
        public string ParentGroup => _parentLocation.Group;

        /// <summary>
        /// Parent group name
        /// </summary>
        public string ParentGroupName => !string.IsNullOrEmpty(_parentLocation.Group)
            ? ((CatalogGroupLocation)Enum.Parse(typeof(CatalogGroupLocation), _parentLocation.Group)).Description()
            : string.Empty;
        /// <summary>
        /// Status name
        /// </summary>
        public string StatusName => Status.Description();

        #endregion

    }
}
