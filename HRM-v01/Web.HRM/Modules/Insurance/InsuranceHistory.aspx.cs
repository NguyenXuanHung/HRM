using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Web.Core;
using Web.Core.Framework;

namespace Web.HRM.Modules.Insurance
{
    public partial class InsuranceHistory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!ExtNet.IsAjaxRequest)
            {
                hdfYearFilter.Text = DateTime.Now.Year.ToString();
                spnYearFilter.SetValue(DateTime.Now.Year);
                // load grid
                LoadGridPanel();
            }
        }

        private void LoadGridPanel()
        {
            // add new column
            AddColumn();
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddColumn()
        {
            // remove fields
            storeInsurance.RemoveFields();

            var view = gpInsurance.View[0];

            //init group header
            var headerGroupRow = new HeaderGroupRow();

            //add column group header
            headerGroupRow.Columns.Add(new HeaderGroupColumn
            {
                Header = "",
                ColSpan = 4,
                Align = Alignment.Center
            });

            // column index
            var colIndex = new RowNumbererColumn
            {
                ColumnID = "colIndex",
                Header = "STT",
                Align = Alignment.Center,

                Width = 40
            };
            gpInsurance.ColumnModel.Columns.Add(colIndex);

            // init column employee name
            var colFullName = new Column
            {
                ColumnID = "colFullName",
                Header = "Họ và tên",
                DataIndex = "FullName",
                Align = Alignment.Left,
                Width = 150
            };
            var recordFieldFullName = new RecordField()
            {
                Name = "FullName",
                Mapping = "FullName"
            };
            storeInsurance.AddField(recordFieldFullName);
            gpInsurance.ColumnModel.Columns.Add(colFullName);

            // column employee code
            var colEmployeeCode = new Column
            {
                ColumnID = "colEmployeeCode",
                Header = "Mã nhân viên",
                DataIndex = "EmployeeCode",
                Align = Alignment.Left,
                Width = 100
            };
            var recordFieldEmployeeCode = new RecordField()
            {
                Name = "EmployeeCode",
                Mapping = "EmployeeCode"
            };
            storeInsurance.AddField(recordFieldEmployeeCode);
            gpInsurance.ColumnModel.Columns.Add(colEmployeeCode);

            //init column departmentName
            var colDepartmentName = new Column
            {
                ColumnID = "colDepartmentName",
                Header = "Đơn vị",
                DataIndex = "DepartmentName",
                Align = Alignment.Left,
                Width = 150
            };
            var recordFieldDepartmentName = new RecordField()
            {
                Name = "DepartmentName",
                Mapping = "DepartmentName"
            };
            storeInsurance.AddField(recordFieldDepartmentName);
            gpInsurance.ColumnModel.Columns.Add(colDepartmentName);

            // init count
            var count = 0;
            var products = new string[] { "Công ty đóng", "Người LĐ đóng" };

            // add column by month
            for(var i = 0; i < 12; i++)
            {
                //add group header
                headerGroupRow.Columns.Add(new HeaderGroupColumn
                {
                    Header = @"Tháng {0}".FormatWith(i + 1),
                    ColSpan = products.Length,
                    Align = Alignment.Center
                });

                // init field
                var recordField = new RecordField()
                {
                    Name = "Month{0}".FormatWith(i + 1),
                    Mapping = "InsuranceDetailModels[{0}]".FormatWith(count++)
                };
                foreach(var product in products)
                {
                    var value = "EnterpriseSocial";
                    if(product == @"Công ty đóng")
                    {
                        value = "EnterpriseSocial";
                    }
                    else
                    {
                        value = "LaborerSocial";
                    }

                    // init column
                    var col = new Column
                    {
                        ColumnID = value,
                        Header = product,
                        DataIndex = recordField.Name,
                        Align = Alignment.Left,
                        Width = 150,
                        Renderer = { Fn = "RenderMonth" }
                    };

                    // add column
                    gpInsurance.ColumnModel.Columns.Add(col);
                }
                // add field
                storeInsurance.AddField(recordField);
            }

            //add header group row
            view.HeaderGroupRows.Add(headerGroupRow);
        }
    }
}