using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.Unit
{
    public static class Config
    {
        public static MySql.Data.MySqlClient.MySqlConnection conn=new MySql.Data.MySqlClient.MySqlConnection(System.Configuration.ConfigurationManager.AppSettings["connStr"].ToString());

       
    }
}