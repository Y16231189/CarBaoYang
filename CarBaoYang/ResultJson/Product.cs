using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.ResultJson
{

    public class ProductsObj
    {
        public string MaintenanceCode { get; set; }
        public List<Productdata> ProductDatas { get; set; }
    }

    public class Productdata
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OEMCategory { get; set; }
        public string SeriesNo { get; set; }
        public string CarzoneNo { get; set; }
        public float RefPrice { get; set; }
        public string Type { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string PartTypeCode { get; set; }
        public string PartTypeName { get; set; }
        public List<Specfeaturedata> SpecFeatureDatas { get; set; }
    }

    public class Specfeaturedata
    {
        public string FeatureType { get; set; }
        public string FeatureValue { get; set; }
        public string FeatureTypeName { get; set; }
    }

}