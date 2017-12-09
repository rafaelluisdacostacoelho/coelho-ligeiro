-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema WeEntrepreneurs
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema WeEntrepreneurs
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `WeEntrepreneurs` DEFAULT CHARACTER SET utf8 ;
USE `WeEntrepreneurs` ;

-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Categories`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Categories` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Categories` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(100) NULL,
  PRIMARY KEY (`Id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Suppliers`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Suppliers` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Suppliers` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(100) NULL,
  `TradingName` VARCHAR(100) NULL,
  `CorporateTaxpayerRegistry` VARCHAR(20) NULL,
  `Email` VARCHAR(100) NULL,
  `Situation` INT NOT NULL,
  `Photo` TINYBLOB NULL,
  `Creation` DATETIME NOT NULL,
  `CategoryId` BINARY(16) NOT NULL,
  `Supplierscol` TINYINT(4) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Supplier_Category_IDX` (`CategoryId` ASC),
  CONSTRAINT `FK_Supplier_Category`
    FOREIGN KEY (`CategoryId`)
    REFERENCES `WeEntrepreneurs`.`Categories` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Customers`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Customers` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Customers` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Email` VARCHAR(45) NULL,
  `Responsible` VARCHAR(45) NULL,
  `Description` VARCHAR(1000) NULL,
  `Creation` DATETIME NOT NULL,
  `SupplierId` BINARY(16) NOT NULL,
  `PersonType` TINYINT(4) NOT NULL,
  `Photo` BLOB NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Customer_Supplier` (`SupplierId` ASC),
  CONSTRAINT `FK_Customer_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Contacts`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Contacts` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Contacts` (
  `Id` BINARY(16) NOT NULL,
  `Description` VARCHAR(100) NULL,
  `Creation` DATETIME NOT NULL,
  `CustomerId` BINARY(16) NOT NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Contact_Customer_IDX` (`CustomerId` ASC),
  INDEX `FK_Contact_Supplier_IDX` (`SupplierId` ASC),
  CONSTRAINT `FK_Contact_Customer`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `WeEntrepreneurs`.`Customers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Contact_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Meetings`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Meetings` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Meetings` (
  `Id` BINARY(16) NOT NULL,
  `Date` DATETIME NULL,
  `Duration` TIME NULL,
  `Local` VARCHAR(45) NULL,
  `Description` VARCHAR(45) NULL,
  `Creation` DATETIME NOT NULL,
  `CustomerId` BINARY(16) NOT NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Meeting_Supplier_IDX` (`SupplierId` ASC),
  INDEX `FK_Meeting_Customer_IDX` (`CustomerId` ASC),
  CONSTRAINT `FK_Meeting_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Meeting_Customer`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `WeEntrepreneurs`.`Customers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Projects`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Projects` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Projects` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(100) NULL,
  `Description` VARCHAR(1000) NOT NULL,
  `Creation` DATETIME NOT NULL,
  `CustomerId` BINARY(16) NOT NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Project_Supplier_IDX` (`SupplierId` ASC),
  INDEX `FK_Project_Customer_IDX` (`CustomerId` ASC),
  CONSTRAINT `FK_Project_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Project_Customer`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `WeEntrepreneurs`.`Customers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Users`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Users` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Users` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Email` VARCHAR(100) NULL,
  `Password` VARCHAR(100) NOT NULL,
  `Creation` DATETIME NOT NULL,
  `Situation` INT NOT NULL,
  `SupplierId` BINARY(16) NOT NULL COMMENT '\n',
  PRIMARY KEY (`Id`),
  INDEX `FK_User_Supplier_IDX` (`SupplierId` ASC),
  CONSTRAINT `FK_User_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Addresses`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Addresses` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Addresses` (
  `Id` BINARY(16) NOT NULL,
  `Description` VARCHAR(100) NULL,
  `State` VARCHAR(45) NULL,
  `City` VARCHAR(100) NULL,
  `District` VARCHAR(100) NULL,
  `Number` VARCHAR(45) NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Address_Supplier_IDX` (`SupplierId` ASC),
  CONSTRAINT `FK_Address_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Events`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Events` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Events` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Local` VARCHAR(45) NULL,
  `Begin` DATETIME NULL,
  `End` DATETIME NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Events_Supplier_IDX` (`SupplierId` ASC),
  CONSTRAINT `FK_Event_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Invoices`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Invoices` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Invoices` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(100) NOT NULL,
  `Value` DECIMAL(10,2) NOT NULL,
  `Recurrence` INT NOT NULL,
  `Date` DATETIME NULL,
  `Description` VARCHAR(1000) NULL,
  `Creation` DATETIME NOT NULL,
  `Situation` INT NOT NULL,
  `CustomerId` BINARY(16) NOT NULL,
  `SupplierId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Invoice_Supplier_IDX` (`SupplierId` ASC),
  INDEX `FK_Invoice_Customer_IDX` (`CustomerId` ASC),
  CONSTRAINT `FK_Invoice_Supplier`
    FOREIGN KEY (`SupplierId`)
    REFERENCES `WeEntrepreneurs`.`Suppliers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_Invoice_Customer`
    FOREIGN KEY (`CustomerId`)
    REFERENCES `WeEntrepreneurs`.`Customers` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `WeEntrepreneurs`.`Steps`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `WeEntrepreneurs`.`Steps` ;

CREATE TABLE IF NOT EXISTS `WeEntrepreneurs`.`Steps` (
  `Id` BINARY(16) NOT NULL,
  `Name` VARCHAR(100) NOT NULL,
  `Description` VARCHAR(1000) NULL,
  `Responsible` VARCHAR(100) NULL,
  `Begin` DATETIME NULL,
  `End` DATETIME NULL,
  `Creation` DATETIME NOT NULL,
  `Situation` INT NOT NULL,
  `ProjectId` BINARY(16) NOT NULL,
  PRIMARY KEY (`Id`),
  INDEX `FK_Step_Project_IDX` (`ProjectId` ASC),
  CONSTRAINT `FK_Step_Project`
    FOREIGN KEY (`ProjectId`)
    REFERENCES `WeEntrepreneurs`.`Projects` (`Id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

