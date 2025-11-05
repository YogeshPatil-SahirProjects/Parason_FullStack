-- Migration: Drop and Recreate QuoteHeader table with Identity on QuoteID
-- This script will:
-- 1. Drop the existing QuoteHeader table (WARNING: This will delete all data!)
-- 2. Recreate it with QuoteID as an IDENTITY column
-- 3. Re-insert seed data

-- Drop the existing QuoteHeader table
IF OBJECT_ID('[dbo].[QuoteHeader]', 'U') IS NOT NULL
BEGIN
    DROP TABLE [dbo].[QuoteHeader];
END
GO

-- Recreate the QuoteHeader table with QuoteID as IDENTITY
CREATE TABLE [dbo].[QuoteHeader] (
    [QuoteID] int IDENTITY(1,1) NOT NULL,
    [QuoteRevision] tinyint NOT NULL DEFAULT 0,
    [QuoteNumber] nvarchar(30) NOT NULL,
    [QuoteName] nvarchar(100) NOT NULL,
    [CustomerName] nvarchar(100) NOT NULL,
    [Status] nvarchar(30) NOT NULL DEFAULT 'Draft',
    [Currency] nvarchar(3) NOT NULL DEFAULT 'INR',
    [ValidityDays] int NOT NULL DEFAULT 30,
    [Notes] nvarchar(2000) NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [CreatedBy] nvarchar(100) NOT NULL,
    [ModifiedAt] datetime2 NULL,
    [ModifiedBy] nvarchar(100) NULL,
    CONSTRAINT [PK_QuoteHeader] PRIMARY KEY ([QuoteID], [QuoteRevision])
);
GO

-- Insert seed data
SET IDENTITY_INSERT [dbo].[QuoteHeader] ON;

INSERT INTO [dbo].[QuoteHeader]
    ([QuoteID], [QuoteRevision], [QuoteNumber], [QuoteName], [CustomerName],
     [Status], [Currency], [ValidityDays], [Notes], [CreatedAt], [CreatedBy],
     [ModifiedAt], [ModifiedBy])
VALUES
    (1, 1, 'Q-2025-001', 'First Demo Quote', 'ABC Industries',
     'Draft', 'INR', 30, 'Seed sample quote', '2025-01-01', 'System',
     '2025-01-01', 'Yogesh Patil'),
    (2, 1, 'Q-2025-002', 'Second Demo Quote', 'XYZ Manufacturing',
     'Approved', 'USD', 45, 'Second seed record', '2025-01-02', 'System',
     '2025-01-01', 'Yogesh Patil');

SET IDENTITY_INSERT [dbo].[QuoteHeader] OFF;
GO

-- Verify the table was created with identity
SELECT
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMNPROPERTY(OBJECT_ID('[dbo].[QuoteHeader]'), COLUMN_NAME, 'IsIdentity') AS IsIdentity
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
    AND TABLE_NAME = 'QuoteHeader'
ORDER BY ORDINAL_POSITION;
GO
