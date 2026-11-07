-- Scripts/001_CriarBanco.sql

USE master;
GO

IF DB_ID(N'InventoryManagementSystem') IS NULL
BEGIN
	CREATE DATABASE InventoryManagementSystem;
END;
GO

USE InventoryManagementSystem;
GO