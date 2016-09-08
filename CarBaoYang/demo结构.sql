2/*
Navicat MySQL Data Transfer

Source Server         : 1
Source Server Version : 50090
Source Host           : localhost:3306
Source Database       : demo

Target Server Type    : MYSQL
Target Server Version : 50090
File Encoding         : 65001

Date: 2016-09-08 10:22:44
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for brand
-- ----------------------------
DROP TABLE IF EXISTS `brand`;
CREATE TABLE `brand` (
  `Code` varchar(150) NOT NULL default '',
  `Description` varchar(150) default NULL,
  `FirstPinYin` varchar(150) default NULL,
  `LogUrl` varchar(150) default NULL,
  `Name` varchar(150) default NULL,
  PRIMARY KEY  (`Code`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for maintenanceitem
-- ----------------------------
DROP TABLE IF EXISTS `maintenanceitem`;
CREATE TABLE `maintenanceitem` (
  `WVMId` varchar(150) default NULL,
  `CategoryCode` varchar(150) default NULL,
  `CycleAction` varchar(150) default NULL,
  `CycleActionDesc` varchar(150) default NULL,
  `CycleKM` varchar(150) default NULL,
  `CycleMonth` varchar(150) default NULL,
  `CycleType` varchar(150) default NULL,
  `HasProducts` varchar(150) default NULL,
  `Id` varchar(150) NOT NULL default '',
  `LyId` varchar(150) default NULL,
  `MaintenanceCode` varchar(150) default NULL,
  `Name` varchar(150) default NULL,
  `SortNum` decimal(12,2) default NULL,
  `TipImgUrl` varchar(150) default NULL,
  `TipUrl` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `index_MaintenanceCode` USING BTREE (`MaintenanceCode`),
  KEY `index_LyId` USING BTREE (`LyId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for maintenanceitemproductmapping
-- ----------------------------
DROP TABLE IF EXISTS `maintenanceitemproductmapping`;
CREATE TABLE `maintenanceitemproductmapping` (
  `Id` varchar(150) NOT NULL default '',
  `ProductId` decimal(12,2) default NULL,
  `MaintenanceCode` varchar(150) default NULL,
  `LyId` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `index_MaintenanceCode` USING BTREE (`MaintenanceCode`),
  KEY `index_LyId` USING BTREE (`LyId`),
  KEY `index_Product` USING BTREE (`ProductId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for manufacturer
-- ----------------------------
DROP TABLE IF EXISTS `manufacturer`;
CREATE TABLE `manufacturer` (
  `BrandCode` varchar(150) default NULL,
  `BrandName` varchar(150) default NULL,
  `Category` varchar(150) default NULL,
  `ManufacturerCode` varchar(150) default NULL,
  `ManufacturerID` varchar(150) NOT NULL default '',
  `ManufacturerName` varchar(150) default NULL,
  `ModelName` varchar(150) default NULL,
  `Serie` varchar(150) default NULL,
  PRIMARY KEY  (`ManufacturerID`),
  KEY `index_ManufacturerCode` USING BTREE (`ManufacturerCode`),
  KEY `index_BrandCode` USING BTREE (`BrandCode`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for model
-- ----------------------------
DROP TABLE IF EXISTS `model`;
CREATE TABLE `model` (
  `Brand` varchar(150) default NULL,
  `LYId` varchar(150) default NULL,
  `Manufacturer` varchar(150) default NULL,
  `ManufacturerCode` varchar(150) default NULL,
  `MaxOutputKW` varchar(150) default NULL,
  `MIITCode` varchar(150) default NULL,
  `ModelName` varchar(150) default NULL,
  `ModelVersion` varchar(150) default NULL,
  `ModelYear` varchar(150) default NULL,
  `PriceReference` varchar(150) default NULL,
  `Serie` varchar(150) default NULL,
  `TyreSpecFront` varchar(150) default NULL,
  `TyreSpecRear` varchar(150) default NULL,
  `WVMId` varchar(150) NOT NULL default '',
  `YearStartProduction` varchar(150) default NULL,
  PRIMARY KEY  (`WVMId`),
  KEY `index_lyID` USING BTREE (`LYId`),
  KEY `index_ManufacturerCode` USING BTREE (`ManufacturerCode`),
  KEY `index_WVMId` USING BTREE (`WVMId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for modeldetails
-- ----------------------------
DROP TABLE IF EXISTS `modeldetails`;
CREATE TABLE `modeldetails` (
  `AccelerationSeconds` varchar(150) default NULL,
  `BodyForm` varchar(150) default NULL,
  `BodyHeightMM` varchar(150) default NULL,
  `BodyLengthMM` varchar(150) default NULL,
  `BodyWidthMM` varchar(150) default NULL,
  `BrakeFront` varchar(150) default NULL,
  `BrakeRear` varchar(150) default NULL,
  `BrandCode` varchar(150) default NULL,
  `CarClass` varchar(150) default NULL,
  `CarryingCapacityKG` varchar(150) default NULL,
  `CarType` varchar(150) default NULL,
  `Category` varchar(150) default NULL,
  `CompressionRatio` varchar(150) default NULL,
  `Doors` varchar(150) default NULL,
  `DrivingForm` varchar(150) default NULL,
  `DrivingWheel` varchar(150) default NULL,
  `Emission` varchar(150) default NULL,
  `EngineCapacityL` varchar(150) default NULL,
  `EngineCapacityML` varchar(150) default NULL,
  `EngineCode` varchar(150) default NULL,
  `EngineCylinders` varchar(150) default NULL,
  `EngineCylinderValves` varchar(150) default NULL,
  `EngineForm` varchar(150) default NULL,
  `EngineFuelSupply` varchar(150) default NULL,
  `EnginePosition` varchar(150) default NULL,
  `FoglightFront` varchar(150) default NULL,
  `FuelClass` varchar(150) default NULL,
  `FuelConsumptionCombinedL` varchar(150) default NULL,
  `FuelConsumptionExtraUrbanL` varchar(150) default NULL,
  `FuelConsumptionUrbanL` varchar(150) default NULL,
  `FuelType` varchar(150) default NULL,
  `GroundClearanceMM` varchar(150) default NULL,
  `HeadlightLED` varchar(150) default NULL,
  `HeadlightXenon` varchar(150) default NULL,
  `LYId` varchar(150) default NULL,
  `ManufacturerCode` varchar(150) default NULL,
  `MaxOutputHP` varchar(150) default NULL,
  `MaxOutputKW` varchar(150) default NULL,
  `MaxOutputRPM` varchar(150) default NULL,
  `MaxTorqueNM` varchar(150) default NULL,
  `MaxTorqueRPM` varchar(150) default NULL,
  `MIITCode` varchar(150) default NULL,
  `MinimumTurningRadiusM` varchar(150) default NULL,
  `ModelName` varchar(150) default NULL,
  `ModelVersion` varchar(150) default NULL,
  `ModelYear` varchar(150) default NULL,
  `MonthStartSelling` varchar(150) default NULL,
  `NationOrigin` varchar(150) default NULL,
  `PlatformCode` varchar(150) default NULL,
  `PriceReference` varchar(150) default NULL,
  `ProductionStatus` varchar(150) default NULL,
  `ProductionType` varchar(150) default NULL,
  `Seats` varchar(150) default NULL,
  `Serie` varchar(150) default NULL,
  `SteeringPowerType` varchar(150) default NULL,
  `SteeringType` varchar(150) default NULL,
  `SuspensionFront` varchar(150) default NULL,
  `SuspensionRear` varchar(150) default NULL,
  `TankCapacityL` varchar(150) default NULL,
  `TopSpeedKPH` varchar(150) default NULL,
  `TransmissionDesc` varchar(150) default NULL,
  `TransmissionGears` varchar(150) default NULL,
  `TransmissionType` varchar(150) default NULL,
  `TrunkCapacityL` varchar(150) default NULL,
  `TurboType` varchar(150) default NULL,
  `TyreSpecFront` varchar(150) default NULL,
  `TyreSpecRear` varchar(150) default NULL,
  `TyreSpecSpare` varchar(150) default NULL,
  `WeightKG` varchar(150) default NULL,
  `WheelbaseMM` varchar(150) default NULL,
  `WheelhubSpecFront` varchar(150) default NULL,
  `WheelhubSpecRear` varchar(150) default NULL,
  `WheelRimMaterial` varchar(150) default NULL,
  `WheelspanFrontMM` varchar(150) default NULL,
  `WheelspanRearMM` varchar(150) default NULL,
  `WVMId` varchar(150) NOT NULL default '',
  `YearShutProduction` varchar(150) default NULL,
  `YearStartProduction` varchar(150) default NULL,
  `YearStartSelling` varchar(150) default NULL,
  PRIMARY KEY  (`WVMId`),
  KEY `index_BrandCode` USING BTREE (`BrandCode`),
  KEY `index_LYId` USING BTREE (`LYId`),
  KEY `index_VMID` USING BTREE (`WVMId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for parttypedata
-- ----------------------------
DROP TABLE IF EXISTS `parttypedata`;
CREATE TABLE `parttypedata` (
  `ID` varchar(150) NOT NULL default '',
  `ChildName` varchar(150) default NULL,
  `Code` varchar(150) default NULL,
  `Name` varchar(150) default NULL,
  `Description` varchar(150) default NULL,
  PRIMARY KEY  (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `Id` decimal(12,2) NOT NULL default '0.00',
  `Name` varchar(150) default NULL,
  `Description` varchar(150) default NULL,
  `OEMCategory` varchar(150) default NULL,
  `SeriesNo` varchar(150) default NULL,
  `CarzoneNo` varchar(150) default NULL,
  `RefPrice` varchar(150) default NULL,
  `Type` varchar(150) default NULL,
  `CategoryId` decimal(12,2) default NULL,
  `CategoryName` varchar(150) default NULL,
  `BrandId` decimal(12,2) default NULL,
  `BrandName` varchar(150) default NULL,
  `SeriesId` decimal(12,2) default NULL,
  `SeriesName` varchar(150) default NULL,
  `PartTypeCode` varchar(150) default NULL,
  `PartTypeName` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `index_id` USING HASH (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for specfeaturedatas
-- ----------------------------
DROP TABLE IF EXISTS `specfeaturedatas`;
CREATE TABLE `specfeaturedatas` (
  `Id` varchar(150) NOT NULL default '',
  `ProductId` decimal(12,2) default NULL,
  `FeatureType` varchar(150) default NULL,
  `FeatureValue` varchar(150) default NULL,
  `FeatureTypeName` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `index_ProductId` USING BTREE (`ProductId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for vimpartspecdata
-- ----------------------------
DROP TABLE IF EXISTS `vimpartspecdata`;
CREATE TABLE `vimpartspecdata` (
  `Id` varchar(150) NOT NULL default '',
  `SpecType` varchar(150) default NULL,
  `SpecValue` varchar(150) default NULL,
  `SpecNameCN` varchar(150) default NULL,
  `MaintenanceItemId` varchar(150) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `index_MaintenanceItemId` USING BTREE (`MaintenanceItemId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
