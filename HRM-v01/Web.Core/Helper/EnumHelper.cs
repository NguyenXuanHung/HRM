using System;
using System.Collections.Generic;

namespace Web.Core
{
    public class EnumHelper
    {
        // catalog group name
        private const string CatalogGroupSex = @"sex";
        private const string CatalogGroupLocation = @"location";
        private const string CatalogGroupBasicEducation = @"basiceducation";
        private const string CatalogGroupEducation = @"education";
        private const string CatalogGroupContractType = @"contracttype";
        private const string CatalogGroupLanguageLevel = @"languagelevel";
        private const string CatalogGroupITLevel = @"itlevel";
        private const string CatalogGroupManagementLevel = @"managementlevel";
        private const string CatalogGroupPoliticLevel = @"politiclevel";
        private const string CatalogGroupEmployeeType = @"employeetype";
        private const string CatalogGroupRecordStatus = @"recordstatus";

        // catalog object name
        private const string CatalogNameLocation = @"cat_Location";
        private const string CatalogNameBasicEducation = @"cat_BasicEducation";
        private const string CatalogNameEducation = @"cat_Education";
        private const string CatalogNameContractType = @"cat_ContractType";
        private const string CatalogNameLanguageLevel = @"cat_LanguageLevel";
        private const string CatalogNameITLevel = @"cat_ITLevel";
        private const string CatalogNameManagementLevel = @"cat_ManagementLevel";
        private const string CatalogNamePoliticLevel = @"cat_PoliticLevel";
        private const string CatalogNameEmployeeType = @"cat_EmployeeType";
        private const string CatalogNameWorkStatus = @"cat_WorkStatus";

        /// <summary>
        /// Get catalog group items
        /// </summary>
        /// <param name="catalogGroupName"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetCatalogGroupItems(string catalogGroupName)
        {
            switch (catalogGroupName.ToLower())
            {
                case CatalogGroupSex:
                    return typeof(CatalogGroupSex).GetValuesAndDescription();
                case CatalogGroupLocation:
                    return typeof(CatalogGroupLocation).GetValuesAndDescription();
                case CatalogGroupBasicEducation:
                    return typeof(CatalogGroupBasicEducation).GetValuesAndDescription();
                case CatalogGroupEducation:
                    return typeof(CatalogGroupEducation).GetValuesAndDescription();
                case CatalogGroupContractType:
                    return typeof(CatalogGroupContractType).GetValuesAndDescription();
                case CatalogGroupLanguageLevel:
                    return typeof(CatalogGroupLanguageLevel).GetValuesAndDescription();
                case CatalogGroupITLevel:
                    return typeof(CatalogGroupITLevel).GetValuesAndDescription();
                case CatalogGroupManagementLevel:
                    return typeof(CatalogGroupManagementLevel).GetValuesAndDescription();
                case CatalogGroupPoliticLevel:
                    return typeof(CatalogGroupPoliticLevel).GetValuesAndDescription();
                case CatalogGroupEmployeeType:
                    return typeof(CatalogGroupEmployeeType).GetValuesAndDescription();
                case CatalogGroupRecordStatus:
                    return typeof(RecordStatus).GetValuesAndDescription();
            }
            return null;
        }

        /// <summary>
        /// Get catalog group name
        /// </summary>
        /// <param name="objName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetCatalogGroupName(string objName, string value)
        {
            if (string.IsNullOrEmpty(objName) || string.IsNullOrEmpty(value))
                return string.Empty;

            switch(objName)
            {
                case CatalogNameLocation:
                    return ((CatalogGroupLocation)Enum.Parse(typeof(CatalogGroupLocation), value)).Description();
                case CatalogNameBasicEducation:
                    return ((CatalogGroupBasicEducation)Enum.Parse(typeof(CatalogGroupBasicEducation), value)).Description();
                case CatalogNameEducation:
                    return ((CatalogGroupEducation)Enum.Parse(typeof(CatalogGroupEducation), value)).Description();
                case CatalogNameContractType:
                    return ((CatalogGroupContractType)Enum.Parse(typeof(CatalogGroupContractType), value)).Description();
                case CatalogNameLanguageLevel:
                    return ((CatalogGroupLanguageLevel)Enum.Parse(typeof(CatalogGroupLanguageLevel), value)).Description();
                case CatalogNameITLevel:
                    return ((CatalogGroupITLevel)Enum.Parse(typeof(CatalogGroupITLevel), value)).Description();
                case CatalogNameManagementLevel:
                    return ((CatalogGroupManagementLevel)Enum.Parse(typeof(CatalogGroupManagementLevel), value)).Description();
                case CatalogNamePoliticLevel:
                    return ((CatalogGroupPoliticLevel)Enum.Parse(typeof(CatalogGroupPoliticLevel), value)).Description();
                case CatalogNameEmployeeType:
                    return ((CatalogGroupEmployeeType)Enum.Parse(typeof(CatalogGroupEmployeeType), value)).Description();
                case CatalogNameWorkStatus:
                    return ((RecordStatus)Enum.Parse(typeof(RecordStatus), value)).Description();
            }
            return string.Empty;
        }

        public static string GetOrderedType(ReportColumnType type)
        {
            var result = string.Empty;
            switch (type)
            {
                case ReportColumnType.Header:
                    result = "1. " + type.Description();
                    break;
                case ReportColumnType.HeaderGroup:
                    result = "2. " + type.Description();
                    break;
                case ReportColumnType.Detail:
                    result = "3. " + type.Description();
                    break;
                case ReportColumnType.FooterGroup:
                    result = "4. " + type.Description();
                    break;
                case ReportColumnType.Footer:
                    result = "5. " + type.Description();
                    break;
            }
            return result;
        }
    }
}
