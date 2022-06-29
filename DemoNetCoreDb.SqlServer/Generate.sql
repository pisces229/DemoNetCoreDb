-- Create Table
CREATE TABLE [dbo].[Favorites](
    [rowId] [int] IDENTITY(1,1) NOT NULL,
    [userKey] [int] NOT NULL,
    [favoriteId] [nvarchar](255) NULL,
    [lastModified] [datetime] NULL,
    [isDeleted] [bit] NULL,
    [created] [datetime] NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED 
(
    [rowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- This is the schema name
DECLARE @Schema VARCHAR(MAX) = 'Dbo'
-- This is the table name. Write your own table name here
DECLARE @Table VARCHAR(MAX) = 'Favorites'
DECLARE @result varchar(max) = ''
SET    @result = @result + 'namespace ' + @Schema  + CHAR(13) + '{' + CHAR(13) 
SET    @result = @result + '    public class ' + @Table + CHAR(13) + '    {' + CHAR(13) 
SELECT @result = @result + '        public ' + DataType + ' ' + PropertyName + ' { get; set; } ' + CHAR(13)
FROM (SELECT
    UPPER(left(c.COLUMN_NAME,1))+SUBSTRING(c.COLUMN_NAME,2,LEN(c.COLUMN_NAME)) AS PropertyName,
    CASE c.DATA_TYPE
        WHEN 'bigint'           THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'long?' ELSE 'long' END
        WHEN 'binary'           THEN 'Byte[]'
        WHEN 'bit'              THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'bool?' ELSE 'bool' END
        WHEN 'char'             THEN 'string'
        WHEN 'date'             THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END
        WHEN 'datetime'         THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END
        WHEN 'datetime2'        THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END
        WHEN 'datetimeoffset'   THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'DateTimeOffset?' ELSE 'DateTimeOffset' END
        WHEN 'decimal'          THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'decimal?' ELSE 'decimal' END
        WHEN 'float'            THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'double?' ELSE 'double' END
        WHEN 'image'            THEN 'Byte[]'
        WHEN 'int'              THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'int?' ELSE 'int' END
        WHEN 'money'            THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'decimal?' ELSE 'decimal' END
        WHEN 'nchar'            THEN 'string'
        WHEN 'ntext'            THEN 'string'
        WHEN 'numeric'          THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'decimal?' ELSE 'decimal' END
        WHEN 'nvarchar'         THEN 'string'
        WHEN 'real'             THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'double?' ELSE 'double' END
        WHEN 'smalldatetime'    THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'DateTime?' ELSE 'DateTime' END
        WHEN 'smallint'         THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'short?' ELSE 'short' END
        WHEN 'smallmoney'       THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'decimal?' ELSE 'decimal' END
        WHEN 'text'             THEN 'string'
        WHEN 'time'             THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'TimeSpan?' ELSE 'TimeSpan' END
        WHEN 'timestamp'        THEN 'Byte[]'
        WHEN 'tinyint'          THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'Byte?' ELSE 'Byte' END
        WHEN 'uniqueidentifier' THEN CASE C.IS_NULLABLE WHEN 'YES' THEN 'Guid?' ELSE 'Guid' END
        WHEN 'varbinary'        THEN 'Byte[]'
        WHEN 'varchar'          THEN 'string'
        ELSE 'Object'
    END AS DataType, c.ORDINAL_POSITION
    FROM INFORMATION_SCHEMA.COLUMNS c
    WHERE c.TABLE_NAME = @Table
    AND ISNULL(@Schema, c.TABLE_SCHEMA) = c.TABLE_SCHEMA) t
ORDER BY t.ORDINAL_POSITION
 
SET @result = @result  + '    }' + CHAR(13)
SET @result = @result + '}'
 
PRINT @result
