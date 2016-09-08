using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.Models
{
    public class MaintenanceitemProductMapping
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string MaintenanceCode { get; set; }
        public string LyId { get; set; }
    }
}