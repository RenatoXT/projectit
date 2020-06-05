CREATE DATABASE projectIt
GO

USE projectIt
GO

CREATE PROCEDURE spDelete
(
	@id INT,
	@table VARCHAR(MAX)
)
AS
BEGIN
	DECLARE @sql VARCHAR(MAX);
	SET @sql = 'DELETE ' + @table +
		' WHERE id = ' + CAST(@id AS VARCHAR(MAX))
	EXEC(@sql)
END
GO

CREATE PROCEDURE spQuery
(
	@id INT,
	@table VARCHAR(MAX)
)
AS 
BEGIN
	DECLARE @sql VARCHAR(MAX)
	SET @sql = 'SELECT * FROM ' + @table + 
		' WHERE id = ' + CAST(@id AS VARCHAR(MAX))
	EXEC(@sql)
END
GO

CREATE PROCEDURE spList
(
	@table VARCHAR(MAX),
	@order VARCHAR(MAX)
)
AS 
BEGIN
	EXEC('SELECT * FROM ' + @table +
		' ORDER BY ' + @order)
END	
GO

CREATE PROCEDURE spNextId
(
	@table VARCHAR(MAX)
)
AS
BEGIN
	EXEC('SELECT ISNULL(MAX(id) +1, 1) AS MAIOR FROM ' 
		+ @table)
END
GO