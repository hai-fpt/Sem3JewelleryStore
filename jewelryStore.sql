-- Database check and creation
DROP DATABASE IF EXISTS JewelryStore;
CREATE DATABASE JewelryStore;
USE JewelryStore;

-- Table structure for AdminLoginMst
DROP TABLE IF EXISTS `AdminLoginMst`;
CREATE TABLE `AdminLoginMst`
(
    `userName` varchar(50)  NOT NULL PRIMARY KEY,
    `Password` varchar(256) NOT NULL
);

-- Table structure for BrandMst
DROP TABLE IF EXISTS `BrandMst`;
CREATE TABLE `BrandMst`
(
    `Brand_ID`   nchar(10)   NOT NULL PRIMARY KEY,
    `Brand_Type` varchar(50) NOT NULL
);

-- Table structure for CatMst
DROP TABLE IF EXISTS `CatMst`;
CREATE TABLE `CatMst`
(
    `Cat_ID`   nchar(10)   NOT NULL PRIMARY KEY,
    `Cat_Name` varchar(50) NOT NULL
);

-- Table structure for CertifyMst
DROP TABLE IF EXISTS `CertifyMst`;
CREATE TABLE `CertifyMst`
(
    `Certify_ID`   nchar(10)   NOT NULL PRIMARY KEY,
    `Certify_Type` varchar(50) NOT NULL
);

-- Table structure for ProdMst
DROP TABLE IF EXISTS `ProdMst`;
CREATE TABLE `ProdMst`
(
    `Prod_ID`   nchar(10)   NOT NULL PRIMARY KEY,
    `Prod_Type` varchar(50) NOT NULL
);

-- Table structure for GoldKrtMst
DROP TABLE IF EXISTS `GoldKrtMst`;
CREATE TABLE `GoldKrtMst`
(
    `GoldType_ID` nchar(10)   NOT NULL PRIMARY KEY,
    `Gold_Crt`    varchar(50) NOT NULL
);


-- Table structure for ItemMst
DROP TABLE IF EXISTS `ItemMst`;
CREATE TABLE `ItemMst`
(
    `Style_Code`   varchar(50) PRIMARY KEY,
    `Pairs`        numeric(3, 0)  NOT NULL,
    `Brand_ID`     nchar(10),
    `Quantity`     numeric(18, 0) NOT NULL,
    `Cat_ID`       nchar(10),
    `Prod_Quality` varchar(50)    NOT NULL,
    `Certify_ID`   nchar(10),
    `Prod_ID`      nchar(10),
    `GoldType_ID`  nchar(10),
    `Gold_Wt`      numeric(10, 3) NOT NULL,
    `Stone_Wt`     numeric(10, 2) NOT NULL,
    `Net_Gold`     numeric(10, 3) NOT NULL,
    `Wstg_Per`     numeric(10, 3) NOT NULL,
    `Wstg`         numeric(10, 3) NOT NULL,
    `Tot_Gross_Wt` numeric(10, 3) NOT NULL,
    `Gold_Rate`    numeric(10, 2) NOT NULL,
    `Gold_Amt`     numeric(10, 2) NOT NULL,
    `Gold_Making`  numeric(10, 2) NOT NULL,
    `Stone_Making` numeric(10, 2) NOT NULL,
    `Other_Making` numeric(10, 2) NOT NULL,
    `Tot_Making`   numeric(10, 2) NOT NULL,
    `MRP`          numeric(10, 2) NOT NULL,
    FOREIGN KEY (Brand_ID) REFERENCES BrandMst (Brand_ID),
    FOREIGN KEY (Cat_ID) REFERENCES CatMst (Cat_ID),
    FOREIGN KEY (Certify_ID) REFERENCES CertifyMst (Certify_ID),
    FOREIGN KEY (Prod_ID) REFERENCES ProdMst (Prod_ID),
    FOREIGN KEY (GoldType_ID) REFERENCES GoldKrtMst (GoldType_ID)
);

-- Table structure for DimInfoMst
DROP TABLE IF EXISTS `DimInfoMst`;
CREATE TABLE `DimInfoMst`
(
    `DimID`      nchar(10)   NOT NULL PRIMARY KEY,
    `DimType`    varchar(50) NOT NULL,
    `DimSubType` varchar(50) NOT NULL,
    `DimCrt`     varchar(50) NOT NULL,
    `DimPrice`   nchar(50)   NOT NULL,
    `DimImg`     varchar(50) NOT NULL
);

-- Table structure for DimQltyMst
DROP TABLE IF EXISTS `DimQltyMst`;
CREATE TABLE `DimQltyMst`
(
    `DimQlty_ID` nchar(10)   NOT NULL PRIMARY KEY,
    `DimQlty`    varchar(50) NOT NULL
);

-- Table structure for DimQltySubMst
DROP TABLE IF EXISTS `DimQltySubMst`;
CREATE TABLE `DimQltySubMst`
(
    `DimSubType_ID` nchar(10)   NOT NULL PRIMARY KEY,
    `DimQlty`       varchar(50) NOT NULL
);

-- Table structure for DimMst
DROP TABLE IF EXISTS `DimMst`;
CREATE TABLE `DimMst`
(
    `Style_Code`    varchar(50),
    `DimID`         nchar(10) PRIMARY KEY ,
    `DimQlty_ID`    nchar(10),
    `DimSubType_ID` nchar(10),
    `Dim_Crt`       numeric(10, 2),
    `Dim_Pcs`       numeric(10, 2),
    `Dim_Gm`        numeric(10, 2),
    `Dim_Size`      numeric(10, 2),
    `Dim_Rate`      numeric(10, 2),
    `Dim_Amt`       numeric(10, 2),
    FOREIGN KEY (Style_Code) REFERENCES ItemMst (Style_Code),
    FOREIGN KEY (DimID) REFERENCES DimInfoMst (DimID),
    FOREIGN KEY (DimQlty_ID) REFERENCES DimQltyMst (DimQlty_ID),
    FOREIGN KEY (DimSubType_ID) REFERENCES DimQltySubMst (DimSubType_ID)
);

-- Table structure for StoneQltyMst
DROP TABLE IF EXISTS `StoneQltyMst`;
CREATE TABLE `StoneQltyMst`
(
    `StoneQlty_ID` nchar(10) NOT NULL PRIMARY KEY,
    `StoneQlty`    varchar(50)
);

-- Table structure for StoneMst
DROP TABLE IF EXISTS `StoneMst`;
CREATE TABLE `StoneMst`
(
    `Style_Code`   varchar(50),
    `StoneQlty_ID` nchar(10),
    `Stone_Gm`     numeric(10, 2),
    `Stone_Pcs`    numeric(10, 2),
    `Stone_Rate`   numeric(10, 2),
    `Stone_Amt`    numeric(10, 2),
    FOREIGN KEY (Style_Code) REFERENCES ItemMst (Style_Code),
    FOREIGN KEY (StoneQlty_ID) REFERENCES StoneQltyMst (StoneQlty_ID)
);

-- Table structure for UserRegMst
DROP TABLE IF EXISTS `UserRegMst`;
CREATE TABLE `UserRegMst`
(
    `userID`    nchar(10) PRIMARY KEY,
    `userFname` text,
    `userLname` text,
    `address`   varchar(255),
    `city`      nvarchar(50),
    `state`     nvarchar(50),
    `mobNo`     text,
    `emailID`   text,
    `dob`       nvarchar(50),
    `cdate`     nvarchar(50),
    `username`  varchar(50) UNIQUE,
    `password`  varchar(256)
);

-- Table structure for Inquiry
DROP TABLE IF EXISTS `Inquiry`;
CREATE TABLE `Inquiry`
(
    `ID`      nchar(10)    NOT NULL PRIMARY KEY,
    `Name`    varchar(50)  NOT NULL,
    `City`    varchar(50)  NOT NULL,
    `Contact` nchar(10)    NOT NULL,
    `EmailID` varchar(50)  NOT NULL,
    `Comment` varchar(255) NOT NULL,
    `Cdate`   DATE         NOT NULL,
    `userID`  nchar(10)    NOT NULL,
    FOREIGN KEY (userID) REFERENCES UserRegMst (userID)
);

-- Table structure for JewelTypeMst
DROP TABLE IF EXISTS `JewelTypeMst`;
CREATE TABLE `JewelTypeMst`
(
    `ID`             nchar(10)   NOT NULL PRIMARY KEY,
    `Jewellery_Type` varchar(50) NOT NULL,
    `Item_ID`        varchar(50) NOT NULL,
    `MRP`          numeric(10, 2) NOT NULL,
    `img_path`       varchar(50) NOT NULL,
    FOREIGN KEY (Item_ID) REFERENCES ItemMst (Style_Code)
);


-- Table structure for CartList
DROP TABLE IF EXISTS `CartList`;
CREATE TABLE `CartList`
(
    `id`       nchar(10)      NOT NULL PRIMARY KEY,
    `title`    varchar(50)    NOT NULL,
    `quantity` varchar(50)    NOT NULL,
    `price`    numeric(10, 2) NOT NULL
);

-- Table structure for mock credit cards
DROP TABLE IF EXISTS `CreditCard`;
CREATE TABLE `CreditCard`
(
    `Card_Number`     varchar(16) NOT NULL PRIMARY KEY,
    `Card_Name`       varchar(50) NOT NULL,
    `Card_CVV`        varchar(10) NOT NULL,
    `Card_Expiration` nchar(10)   NOT NULL
);

-- Table structure for Income
DROP TABLE IF EXISTS `Income`;
CREATE TABLE `Income`
(
    `Order_Id` varchar(50) NOT NULL PRIMARY KEY,
    `Amount`        varchar(50) NOT NULL
);