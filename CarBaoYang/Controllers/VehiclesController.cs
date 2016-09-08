using CarBaoYang.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Dapper;
using System.Data;
using CarBaoYang.ResultJson;
using CarBaoYang.Cache;

namespace CarBaoYang.Controllers
{
    public class VehiclesController : ApiController
    {
        public IList<ResultJson.Brands> GetBrands()
        {
            return Cache.BrandManage.Data;
        }
        public List<ResultJson.manufacturer> GetManufacturerByBrand(string id)
        {
            List<ResultJson.manufacturer> result = Cache.ManufacturerManage.Data.Where(p => p.BrandCode == id).ToList();
            return result;
        }
        [HttpGet]
        public List<ResultJson.CarModel> Manufacturer(string ManufacturerCode, string ModelName)
        {
            //if (Config.conn.State != ConnectionState.Open)
            //{
            //    Config.conn.Open();
            //}
            List<ResultJson.CarModel> result = CarModelManage.Data.Where(p => p.ManufacturerCode == ManufacturerCode && p.ModelName == ModelName).ToList();
            //string sqlCmd = "select * from model where ManufacturerCode='" + ManufacturerCode + "' and ModelName='" + ModelName + "'";
            //List<ResultJson.CarModel> result = Config.conn.Query<ResultJson.CarModel>(sqlCmd).ToList();
            //Config.conn.Close();
            return result;
        }
        [HttpGet]
        public ResultJson.ModelDetails LYId(string id)
        {
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            string sqlCmd = "select * from modeldetails where LYId='" + id + "'";

            Models.ModelDetails tmp = Config.conn.Query<Models.ModelDetails>(sqlCmd).ToList()[0];
            sqlCmd = "select * from brand where Code='" + tmp.BrandCode + "'";
            ResultJson.ModelDetails result = tmp.ToMapper<ResultJson.ModelDetails>();
            ResultJson.Brands brands = Config.conn.Query<ResultJson.Brands>(sqlCmd).ToList()[0];
            result.VehicleBrandData = new ResultJson.Vehiclebranddata(brands);
            sqlCmd = "select * from manufacturer where ManufacturerCode='" + tmp.ManufacturerCode + "'";
            ResultJson.manufacturer manufacturer = Config.conn.Query<ResultJson.manufacturer>(sqlCmd).ToList()[0];
            result.ManufacturerData = new ResultJson.Manufacturerdata(manufacturer);
            Config.conn.Close();
            return result;
        }
        [HttpGet]
        public List<ResultJson.maintenanceitem> MaintenanceItems(string id)
        {
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            string sqlCmd = "select * from maintenanceitem where WVMId='" + id + "'";
            List<ResultJson.maintenanceitem> result = Config.conn.Query<ResultJson.maintenanceitem>(sqlCmd).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].VimPartSpecDatas = Config.conn.Query<ResultJson.Vimpartspecdata>("select * from vimpartspecdata where MaintenanceItemId='" + result[i].Id + "'").ToList();
                result[i].PartTypeData = Config.conn.Query<ResultJson.Parttypedata>("select * from parttypedata where ChildName='" + result[i].Name + "'").ToList()[0];
            }
            Config.conn.Close();
            return result;

        }
        [HttpGet]
        public ProductsObj Products(string LyId, string MaintenanceCode)
        {
            ProductsObj obj = new ProductsObj();
            obj.MaintenanceCode = MaintenanceCode;
            obj.ProductDatas = new List<Productdata>();
            if (Config.conn.State != ConnectionState.Open)
            {
                Config.conn.Open();
            }
            List<Models.MaintenanceitemProductMapping> mapping = Config.conn.Query<Models.MaintenanceitemProductMapping>("select * from maintenanceitemproductmapping where MaintenanceCode='" + MaintenanceCode + "' and LyId='" + LyId + "'").ToList();
            foreach (var item in mapping)
            {
                int productId = item.ProductId;
                ResultJson.Productdata data = Config.conn.Query<ResultJson.Productdata>("select * from product where id=" + productId).ToList()[0];
                data.SpecFeatureDatas = Config.conn.Query<ResultJson.Specfeaturedata>("select * from specfeaturedatas where ProductId=" + productId).ToList();
                obj.ProductDatas.Add(data);
            }
            Config.conn.Close();
            return obj;
        }
    }
}
