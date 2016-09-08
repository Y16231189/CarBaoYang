using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarBaoYang.ResultJson
{

    public class ModelDetails
    {
        public string BrandCode { get; set; }
        public string WVMId { get; set; }
        public string LYId { get; set; }
        public string Category { get; set; }
        public string Serie { get; set; }
        public string ModelName { get; set; }
        public string ModelVersion { get; set; }
        public string ModelYear { get; set; }
        public string Emission { get; set; }
        public string CarType { get; set; }
        public string CarClass { get; set; }
        public string PriceReference { get; set; }
        public string YearStartSelling { get; set; }
        public string MonthStartSelling { get; set; }
        public string YearStartProduction { get; set; }
        public string YearShutProduction { get; set; }
        public string MIITCode { get; set; }
        public string EngineCode { get; set; }
        public string PlatformCode { get; set; }
        public string ProductionStatus { get; set; }
        public string NationOrigin { get; set; }
        public string ProductionType { get; set; }
        public string EngineCapacityML { get; set; }
        public string EngineCapacityL { get; set; }
        public string TurboType { get; set; }
        public string FuelType { get; set; }
        public string FuelClass { get; set; }
        public string MaxOutputHP { get; set; }
        public string MaxOutputKW { get; set; }
        public string MaxOutputRPM { get; set; }
        public string MaxTorqueNM { get; set; }
        public string MaxTorqueRPM { get; set; }
        public string EngineForm { get; set; }
        public string EngineCylinders { get; set; }
        public string EngineCylinderValves { get; set; }
        public string CompressionRatio { get; set; }
        public string EngineFuelSupply { get; set; }
        public string FuelConsumptionCombinedL { get; set; }
        public string FuelConsumptionUrbanL { get; set; }
        public string FuelConsumptionExtraUrbanL { get; set; }
        public string AccelerationSeconds { get; set; }
        public string TopSpeedKPH { get; set; }
        public string TransmissionType { get; set; }
        public string TransmissionDesc { get; set; }
        public string TransmissionGears { get; set; }
        public string BrakeFront { get; set; }
        public string BrakeRear { get; set; }
        public string SuspensionFront { get; set; }
        public string SuspensionRear { get; set; }
        public string SteeringType { get; set; }
        public string SteeringPowerType { get; set; }
        public string GroundClearanceMM { get; set; }
        public string MinimumTurningRadiusM { get; set; }
        public string EnginePosition { get; set; }
        public string DrivingWheel { get; set; }
        public string DrivingForm { get; set; }
        public string BodyForm { get; set; }
        public string BodyLengthMM { get; set; }
        public string BodyWidthMM { get; set; }
        public string BodyHeightMM { get; set; }
        public string WheelbaseMM { get; set; }
        public string WheelspanFrontMM { get; set; }
        public string WheelspanRearMM { get; set; }
        public string WeightKG { get; set; }
        public string CarryingCapacityKG { get; set; }
        public string TankCapacityL { get; set; }
        public string TrunkCapacityL { get; set; }
        public string Doors { get; set; }
        public string Seats { get; set; }
        public string TyreSpecFront { get; set; }
        public string TyreSpecRear { get; set; }
        public string WheelhubSpecFront { get; set; }
        public string WheelhubSpecRear { get; set; }
        public string WheelRimMaterial { get; set; }
        public string TyreSpecSpare { get; set; }
        public string HeadlightXenon { get; set; }
        public string HeadlightLED { get; set; }
        public string FoglightFront { get; set; }
        public Vehiclebranddata VehicleBrandData { get; set; }
        public Manufacturerdata ManufacturerData { get; set; }
    }

    public class Vehiclebranddata
    {
        public Vehiclebranddata(Brands data)
        {
            this.Code = data.Code;
            this.Description = data.Description;
            this.FirstPinYin = data.FirstPinYin;
            this.LogUrl = data.LogUrl;
            this.Name = data.Name;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogUrl { get; set; }
        public string FirstPinYin { get; set; }
    }

    public class Manufacturerdata
    {
        public Manufacturerdata(manufacturer data)
        {
            this.Code = data.BrandCode;
            this.Name = data.ManufacturerName;
            this.Description = "";
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}