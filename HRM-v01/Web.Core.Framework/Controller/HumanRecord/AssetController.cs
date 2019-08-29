using System.Collections.Generic;
using Web.Core.Object.HumanRecord;
using Web.Core.Service.HumanRecord;

namespace Web.Core.Framework
{
    /// <summary>
    /// Summary description for AssetController
    /// </summary>
    public class AssetController
    {   
        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AssetModel GetById(int id)
        {
            var recordAsset = hr_AssetServices.GetById(id);
            return new AssetModel(recordAsset);
        }

        /// <summary>
        /// get all asset by record id
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public static List<AssetModel> GetAll(int? recordId)
        {
            var assetModels = new List<AssetModel>();
            var assets = hr_AssetServices.GetAll(recordId);
            foreach (var asset in assets)
            {
                assetModels.Add(new AssetModel(asset));
            }
            return assetModels;
        }

        /// <summary>
        /// update asset
        /// </summary>
        /// <param name="assset"></param>
        public void Update(hr_Asset assset)
        {
            var record = hr_AssetServices.GetById(assset.Id);
            if (record != null)
            {
                record.ReceiveDate = assset.ReceiveDate;
                record.RecordId = assset.RecordId;
                record.Note = assset.Note;
                record.Quantity = assset.Quantity;
                record.Status = assset.Status;
                record.AssetCode = assset.AssetCode;
                record.AssetName = assset.AssetName;
                record.UnitCode = assset.UnitCode;
                record.DeliveryDate = assset.DeliveryDate;
                record.AttachFileName = assset.AttachFileName;
                record.CreatedDate = assset.CreatedDate;
                record.EditedDate = assset.EditedDate;
                hr_AssetServices.Update(assset);
            }
        }

        /// <summary>
        /// insert asset
        /// </summary>
        /// <param name="asset"></param>
        public void Insert(hr_Asset asset)
        {
            hr_AssetServices.Create(asset);
        }

        public static void Delete(int id)
        {
            hr_AssetServices.Delete(id);
        }
    }
}
