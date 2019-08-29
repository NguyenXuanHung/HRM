using System;
using System.Linq;

namespace Web.Core.Framework.Report.Base
{
    public class FilterInput
    {
        public string FilterName { get; set; }
        public string CategoryObjectName { get; set; }
        public string CategoryIdName { get; set; }
    }

    public enum GenerateFilterType
    {
        // record ( hr_resource )
        Rc,

        // contract ( hr_contract )
        Hc,

        // training ( hr_traininghistory)
        Th,

        // education ( hr_educationhistory)
        Eh,

        // salary ( hr_salary )
        Hs,

        // reward (hr_Reward)
        Hr,

        // discipline (hr_Discipline)
        Hd
    }

    public static class FilterGenerate
    {
        private const string PrefixRecordId = "rc.";
        private const string PrefixContractId = "hc.";
        private const string PrefixTrainingId = "th.";
        private const string PrefixEducationId = "eh.";
        private const string PrefixSalary = "hs.";
        private const string PrefixReward = "hr.";
        private const string PrefixDiscipline = "hd.";

        /// <summary>
        /// Category type filter
        /// </summary>
        /// <returns></returns>
        private static FilterItem GenerateFilter(FilterInput filterInput, string prefix, bool isUseStore)
        {
            var filter = new FilterItem
            {
                Name = filterInput.FilterName,
            };
            var categories = CatalogController.GetAll(filterInput.CategoryObjectName, null, null, null, false, null, null).OrderBy(o => o.Name);
            foreach(var item in categories)
            {
                filter.Items.Add(new FilterCondition(item.Name, (isUseStore ? prefix + filterInput.CategoryIdName : "[" + filterInput.CategoryIdName + "]") + " = {0}".FormatWith(item.Id)));
            }
            return filter;
        }

        private static FilterItem GenerateFilter(FilterInput filterInput, GenerateFilterType generateFilterType = GenerateFilterType.Rc, bool isUseStore = true)
        {
            string prefix;
            switch(generateFilterType)
            {
                case GenerateFilterType.Rc:
                    prefix = PrefixRecordId; break;
                case GenerateFilterType.Hc:
                    prefix = PrefixContractId; break;
                case GenerateFilterType.Th:
                    prefix = PrefixTrainingId; break;
                case GenerateFilterType.Eh:
                    prefix = PrefixEducationId; break;
                case GenerateFilterType.Hs:
                    prefix = PrefixSalary; break;
                case GenerateFilterType.Hr:
                    prefix = PrefixReward; break;
                case GenerateFilterType.Hd:
                    prefix = PrefixDiscipline; break;
                default:
                    prefix = "rc."; break;
            }

            return GenerateFilter(filterInput, prefix, isUseStore);
        }

        /// <summary>
        /// Position filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem PositionFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Chức vụ",
                CategoryObjectName = "cat_Position",
                CategoryIdName = "PositionId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Job title filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem JobTitleFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Chức danh",
                CategoryObjectName = "cat_JobTitle",
                CategoryIdName = "JobTitleId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Department filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem DepartmentFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Đơn vị",
                CategoryObjectName = "cat_Department",
                CategoryIdName = "DepartmentId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Folk filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem FolkFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Dân tộc",
                CategoryObjectName = "cat_Folk",
                CategoryIdName = "FolkId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// CPV position filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem CPVPositionFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Chức vụ đảng",
                CategoryObjectName = "cat_CPVPosition",
                CategoryIdName = "CPVPositionId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// VUY position filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem VYUPositionFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Chức vụ đoàn",
                CategoryObjectName = "cat_VYUPosition",
                CategoryIdName = "VYUPositionId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Contract status filter        
        /// </summary>
        /// <returns></returns>
        public static FilterItem ContractStatusFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trạng thái hợp đồng",
                CategoryObjectName = "cat_ContractStatus",
                CategoryIdName = "ContractStatusId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Contract type filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem ContractTypeFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Loại hợp đồng",
                CategoryObjectName = "cat_ContractType",
                CategoryIdName = "ContractTypeId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// IT level filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem ItLevelFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trình độ tin học",
                CategoryObjectName = "cat_ITLevel",
                CategoryIdName = "ITLevelId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Manage level filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem ManagementLevelFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trình độ quản lý",
                CategoryObjectName = "cat_ManagementLevel",
                CategoryIdName = "ManagementLevelId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Education filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem EducationFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trình độ chuyên môn",
                CategoryObjectName = "cat_Education",
                CategoryIdName = "EducationId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Language level filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem LanguageLevelFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trình độ ngoại ngữ",
                CategoryObjectName = "cat_LanguageLevel",
                CategoryIdName = "LanguageLevelId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Politic level filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem PoliticLevelFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trình độ chính trị",
                CategoryObjectName = "cat_PoliticLevel",
                CategoryIdName = "PoliticLevelId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Quantum filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem QuantumFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Tên ngạch",
                CategoryObjectName = "cat_Quantum",
                CategoryIdName = "QuantumId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Family policy filter 
        /// </summary>
        /// <returns></returns>
        public static FilterItem FamilyPolicyFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Gia đình chính sách",
                CategoryObjectName = "cat_FamilyPolicy",
                CategoryIdName = "FamilyPolicyId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Army level filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem ArmyLevelFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Cấp bậc quân nhân",
                CategoryObjectName = "cat_ArmyLevel",
                CategoryIdName = "ArmyLevelId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateFilterType"></param>
        /// <param name="isUseStore"></param>
        /// <returns></returns>
        public static FilterItem NationFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Quốc gia",
                CategoryObjectName = "cat_Nation",
                CategoryIdName = "NationId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateFilterType"></param>
        /// <param name="isUseStore"></param>
        /// <returns></returns>
        public static FilterItem TrainingSystemFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Loại hình đào tạo",
                CategoryObjectName = "cat_TrainingSystem",
                CategoryIdName = "TrainingSystemId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="generateFilterType"></param>
        /// <param name="isUseStore"></param>
        /// <returns></returns>
        public static FilterItem UniversityFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Trường đào tạo",
                CategoryObjectName = "cat_University",
                CategoryIdName = "UniversityId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Reward filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem RewardFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Hình thức khen thưởng",
                CategoryObjectName = "cat_Reward",
                CategoryIdName = "FormRewardId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Discipline filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem DisciplineFilter(bool isUseStore = true, GenerateFilterType generateFilterType = GenerateFilterType.Rc)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Hình thức kỉ luật",
                CategoryObjectName = "cat_Discipline",
                CategoryIdName = "FormDisciplineId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Level reward filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem LevelRewardDisciplineFilter(GenerateFilterType generateFilterType = GenerateFilterType.Rc, bool isUseStore = true)
        {
            return GenerateFilter(new FilterInput
            {
                FilterName = "Hình thức khen thưởng",
                CategoryObjectName = "cat_LevelRewardDiscipline",
                CategoryIdName = "RewardFormatId"
            }, generateFilterType, isUseStore);
        }

        /// <summary>
        /// Sex filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem SexFilter(bool isUseStore = true)
        {
            var sexFilter = new FilterItem
            {
                Name = @"Giới tính",
            };
            // insert values
            if(isUseStore)
            {
                sexFilter.Items.Add(new FilterCondition("Nam", "rc.Sex = 1"));
                sexFilter.Items.Add(new FilterCondition("Nữ", "rc.Sex = 0"));
            }
            else
            {
                sexFilter.Items.Add(new FilterCondition("Nam", "[Sex] = 1"));
                sexFilter.Items.Add(new FilterCondition("Nữ", "[Sex] = 0"));
            }

            return sexFilter;
        }

        /// <summary>
        /// Group contract type filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem GroupContractTypeFilter()
        {
            // filter contract type
            var contractFilter = new FilterItem
            {
                Name = @"Nhóm hợp đồng",
            };
            // insert values
            contractFilter.Items.Add(new FilterCondition("Không xác định thời hạn", "(SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'KXDTH'"));
            contractFilter.Items.Add(new FilterCondition("Xác định thời hạn", "(SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'XDTH'"));
            contractFilter.Items.Add(new FilterCondition("Hợp đồng mùa vụ", "(SELECT TOP 1 cc.[Group] FROM cat_ContractType cc LEFT JOIN hr_Contract hc ON cc.Id = hc.ContractTypeId WHERE hc.RecordId = rc.Id) = 'HDTV'"));
            return contractFilter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static FilterItem GroupEducationFilter()
        {
            var eduFilter = new FilterItem
            {
                Name = @"Trình độ chuyên môn",
            };
            // insert values
            eduFilter.Items.Add(new FilterCondition("Đại học", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'DH'"));
            eduFilter.Items.Add(new FilterCondition("Cao đẳng", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'CD'"));
            eduFilter.Items.Add(new FilterCondition("Trung cấp", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'TC'"));
            eduFilter.Items.Add(new FilterCondition("Sơ cấp", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'SC'"));
            eduFilter.Items.Add(new FilterCondition("Dạy nghề thường xuyên", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'CQDT'"));
            eduFilter.Items.Add(new FilterCondition("Chưa qua đào tạo", "(SELECT TOP 1 [Group] FROM cat_Education DT WHERE DT.Id = rc.EducationId) = 'DNTX'"));

            return eduFilter;
        }

        /// <summary>
        /// Birth year filter
        /// </summary>
        /// <returns></returns>
        public static FilterItem BirthYearFilter()
        {
            var birthYearFilter = new FilterItem
            {
                Name = @"Năm sinh",
            };
            // insert values
            var year = DateTime.Now.AddYears(-10).Year;
            for(var i = 0; i < 50; i++)
            {
                birthYearFilter.Items.Add(new FilterCondition(year.ToString(), "YEAR(rc.BirthDate) = {0}".FormatWith(year)));
                year--;
            }
            return birthYearFilter;
        }
    }
}
