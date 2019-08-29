using Ext.Net;
using System;
using Web.Core.Framework;

namespace Web.HRM.Modules.Catalog
{
    public partial class CatalogRelationship : BasePage
    {
        /// <summary>
        /// Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Init setting window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitWindow(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if(int.TryParse(param, out var id))
                {
                    // init window props
                    if(id > 0)
                    {
                        // edit
                        wdSetting.Title = @"Sửa";
                        wdSetting.Icon = Icon.Pencil;
                    }
                    else
                    {
                        // insert
                        wdSetting.Title = @"Thêm mới";
                        wdSetting.Icon = Icon.Add;
                    }
                    // init id
                    hdfId.Text = id.ToString();
                    // init object
                    var entity = new CatalogRelationshipModel();
                    // check id
                    if(id > 0)
                    {
                        var result = CatalogRelationshipController.GetById(id);
                        if(result != null)
                            entity = result;
                    }
                    // set catalog props
                    // name
                    txtName.Text = entity.Name;
                    // description
                    txtDescription.Text = entity.Description;
                    chkWifeOrHusband.Checked = entity.HasHusband;
                    // order
                    txtOrder.Text = entity.Order.ToString();
                    // show window
                    wdSetting.Show();
                }
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Insert or Update Catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InsertOrUpdate(object sender, DirectEventArgs e)
        {
            try
            {
                // init entity
                var model = new CatalogRelationshipModel();
                // check id
                if(!string.IsNullOrEmpty(hdfId.Text) && Convert.ToInt32(hdfId.Text) > 0)
                {
                    var result = CatalogRelationshipController.GetById(Convert.ToInt32(hdfId.Text)); ;
                    if(result != null)
                        model = result;
                }
                // set new props for entity
                model.Name = txtName.Text;
                model.Description = txtDescription.Text;
                model.Order = !string.IsNullOrEmpty(txtOrder.Text) ? Convert.ToInt32(txtOrder.Text) : 0;
                model.HasHusband = chkWifeOrHusband.Checked;
                // check entity id
                if(model.Id > 0)
                {
                    // update
                    CatalogRelationshipController.Update(model);
                }
                else
                {
                    // insert
                    CatalogRelationshipController.Create(model);
                }
                // hide window
                wdSetting.Hide();
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }

        /// <summary>
        /// Delete catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {
            try
            {
                // init id
                var param = e.ExtraParams["Id"];
                // parse id
                if(!int.TryParse(param, out var id) || id <= 0)
                {
                    // parse error, show error
                    Dialog.ShowError("Có lỗi xảy ra trong quá trình xử lý");
                    return;
                }
                // delete
                CatalogRelationshipController.Delete(id);
                // reload data
                gpCatalog.Reload();
            }
            catch(Exception exception)
            {
                Dialog.ShowError(exception);
            }
        }
    }
}
