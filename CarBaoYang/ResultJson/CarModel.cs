using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.ResultJson
{
    public class CarModel
    {
        public string WVMId { get; set; }
        public string LYId { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerCode { get; set; }
        public string Brand { get; set; }
        public string Serie { get; set; }
        public string ModelName { get; set; }
        public string ModelVersion { get; set; }
        public string ModelYear { get; set; }
        public string PriceReference { get; set; }
        public string YearStartProduction { get; set; }
        public string MIITCode { get; set; }
        public string MaxOutputKW { get; set; }
        public string TyreSpecFront { get; set; }
        public string TyreSpecRear { get; set; }
    }

}