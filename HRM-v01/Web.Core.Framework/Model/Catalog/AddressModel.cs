using Web.Core.Object.Catalog;
using Web.Core.Service.Catalog;

namespace Web.Core.Framework
{
    public class AddressModel
    {
        /// <summary>
        /// Ctor with location name
        /// </summary>
        /// <param name="address"></param>
        /// <param name="wardName"></param>
        /// <param name="districtName"></param>
        /// <param name="provinceName"></param>
        public AddressModel(string address, string wardName, string districtName, string provinceName)
        {
            // init address
            Address = GenerateAddress(address, wardName, districtName, provinceName);
        }

        /// <summary>
        /// Ctor with location id
        /// </summary>
        /// <param name="address"></param>
        /// <param name="wardId"></param>
        /// <param name="districtId"></param>
        /// <param name="proviceId"></param>
        public AddressModel(string address, int wardId, int districtId, int proviceId)
        {
            // get catalog
            var ward = cat_LocationServices.GetById(wardId) ?? new cat_Location();
            var district = cat_LocationServices.GetById(districtId) ?? new cat_Location();
            var province = cat_LocationServices.GetById(proviceId) ?? new cat_Location();

            // init address
            Address = GenerateAddress(address, ward.Name, district.Name, province.Name);
        }

        /// <summary>
        /// Ctor with location object
        /// </summary>
        /// <param name="address"></param>
        /// <param name="ward"></param>
        /// <param name="district"></param>
        /// <param name="province"></param>
        public AddressModel(string address, cat_Location ward, cat_Location district, cat_Location province)
        {
            // get catalog
            ward = ward ?? new cat_Location();
            district = district ?? new cat_Location();
            province = province ?? new cat_Location();

            // init address
            Address = GenerateAddress(address, ward.Name, district.Name, province.Name);
        }
        
        /// <summary>
        /// Display address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Generate address from location
        /// </summary>
        /// <param name="address"></param>
        /// <param name="wardName"></param>
        /// <param name="districtName"></param>
        /// <param name="provinceName"></param>
        /// <returns></returns>
        private string GenerateAddress(string address, string wardName, string districtName, string provinceName)
        {
            if (!string.IsNullOrEmpty(wardName))
                address = string.IsNullOrEmpty(address)
                    ? wardName
                    : "{0}, {1}".FormatWith(address, wardName);
            if (!string.IsNullOrEmpty(districtName))
                address = string.IsNullOrEmpty(address)
                    ? districtName
                    : "{0}, {1}".FormatWith(address, districtName);
            if (!string.IsNullOrEmpty(provinceName))
                address = string.IsNullOrEmpty(address)
                    ? provinceName
                    : "{0}, {1}".FormatWith(address, provinceName);
            return address;
        }
    }
}