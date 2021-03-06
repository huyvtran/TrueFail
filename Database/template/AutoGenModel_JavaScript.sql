DECLARE @TableName sysname = 'MDM_UserProfile'; --table Name
DECLARE @Result VARCHAR(MAX) = 'export class ' + @TableName + '
{';

SELECT @Result = @Result + '
    public ' + ColumnName  + ': '  + ColumnType + ';
'
FROM
(
    SELECT REPLACE(col.name, ' ', '_') ColumnName,
           column_id ColumnId,
           CASE typ.name
               WHEN 'bigint' THEN
                   'number'
               WHEN 'binary' THEN
                   'any'
               WHEN 'bit' THEN
                   'boolean'
               WHEN 'char' THEN
                   'string'
               WHEN 'date' THEN
                   'string'
               WHEN 'datetime' THEN
                   'string'
               WHEN 'datetime2' THEN
                   'string'
               WHEN 'datetimeoffset' THEN
                   'string'
               WHEN 'decimal' THEN
                   'number'
               WHEN 'float' THEN
                   'number'
               WHEN 'int' THEN
                   'number'
               WHEN 'money' THEN
                   'number'
               WHEN 'nchar' THEN
                   'string'
               WHEN 'ntext' THEN
                   'string'
               WHEN 'numeric' THEN
                   'number'
               WHEN 'nvarchar' THEN
                   'string'
               WHEN 'real' THEN
                   'number'
               WHEN 'smalldatetime' THEN
                   'string'
               WHEN 'smallint' THEN
                   'number'
               WHEN 'smallmoney' THEN
                   'number'
               WHEN 'text' THEN
                   'string'
               WHEN 'time' THEN
                   'string'
               WHEN 'timestamp' THEN
                   'string'
               WHEN 'tinyint' THEN
                   'number'
               WHEN 'uniqueidentifier' THEN
                   'string'
               WHEN 'varbinary' THEN
                   'any'
               WHEN 'varchar' THEN
                   'string'
               ELSE
                   'any'
           END ColumnType
    FROM sys.columns col
        JOIN sys.types typ
            ON col.system_type_id = typ.system_type_id
               AND col.user_type_id = typ.user_type_id
    WHERE object_id = OBJECT_ID(@TableName)
) t
ORDER BY ColumnId;

SET @Result = @Result + '
}';

PRINT @Result;