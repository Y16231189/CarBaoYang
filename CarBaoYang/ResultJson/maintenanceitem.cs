using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.ResultJson
{
    public class maintenanceitem
    {
        public string Id { get; set; }
        public string MaintenanceCode { get; set; }
        public string Name { get; set; }
        public string LyId { get; set; }
        public string CycleKM { get; set; }
        public string CycleMonth { get; set; }
        public string CategoryCode { get; set; }
        public string CycleType { get; set; }
        public string CycleAction { get; set; }
        public string CycleActionDesc { get; set; }
        public string TipImgUrl { get; set; }
        public string TipUrl { get; set; }
        public int SortNum { get; set; }
        public Parttypedata PartTypeData { get; set; }
        public List<Vimpartspecdata> VimPartSpecDatas { get; set; }
        public bool HasProducts { get; set; }
    }

    public class Parttypedata
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Vimpartspecdata
    {
        public string Id { get; set; }
        public string SpecType { get; set; }
        public string SpecValue { get; set; }
        public string SpecNameCN { get; set; }
    }

}