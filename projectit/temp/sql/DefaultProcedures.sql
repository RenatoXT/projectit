CREATE DATABASE projectIt
GO

USE projectIt
GO

CREATE TABLE tbUsers (
	users_id INT IDENTITY,
	users_name VARCHAR(MAX),
	users_picture VARBINARY(MAX),
	users_nickname VARCHAR(20) UNIQUE NOT NULL,
	users_email VARCHAR(MAX) NOT NULL,
	users_password VARCHAR(MAX) NOT NULL,
	users_created_at DATE,
	users_updated_at DATE

	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ( [users_id] ASC )WITH (
	pad_index = OFF, statistics_norecompute = OFF, ignore_dup_key = OFF,
	allow_row_locks = on, allow_page_locks = on) ON [PRIMARY]
)ON [PRIMARY]
GO

CREATE TABLE tbTeam (
	team_id INT IDENTITY,
	team_picture VARBINARY(MAX),
	team_name VARCHAR(MAX) NOT NULL,
	team_skill VARCHAR(50),
	team_created_at DATE,
	team_updated_at DATE

	CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ( [team_id] ASC )WITH (
	pad_index = OFF, statistics_norecompute = OFF, ignore_dup_key = OFF,
	allow_row_locks = on, allow_page_locks = on) ON [PRIMARY]
)ON [PRIMARY]
GO

CREATE TABLE tbProject (
	project_id INT IDENTITY,
	project_code VARCHAR(MAX),
	project_picture VARBINARY(MAX),
	project_description VARCHAR(50),
	project_created_at DATE,
	project_updated_at DATE

	CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ( [project_id] ASC )WITH (
	pad_index = OFF, statistics_norecompute = OFF, ignore_dup_key = OFF,
	allow_row_locks = on, allow_page_locks = on) ON [PRIMARY]
)ON [PRIMARY]
GO

CREATE TABLE tbPostIt (
	postit_id INT IDENTITY,
	postit_header VARCHAR(MAX),
	postit_body VARCHAR(MAX) NOT NULL,
	postit_doing_by VARCHAR(50),
	postit_status VARCHAR(50) NOT NULL,
	postit_created_at DATE,
	postit_updated_at DATE

	CONSTRAINT [PK_PostIt] PRIMARY KEY CLUSTERED ( [postit_id] ASC )WITH (
	pad_index = OFF, statistics_norecompute = OFF, ignore_dup_key = OFF,
	allow_row_locks = on, allow_page_locks = on) ON [PRIMARY]
)ON [PRIMARY]
GO

CREATE TABLE tbUsers_Team (
	users_id INT,
	team_id INT	
)
GO

CREATE TABLE tbTeam_Project (
	users_id INT,
	project_id INT
)
GO

CREATE TABLE tbProject_PostIt (
	project_id INT,
	postit_id INT
)
GO

ALTER TABLE [dbo].[tbProject_PostIt] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_project_id] FOREIGN KEY([project_id]) 
  REFERENCES [dbo].[tbProject] ([project_id]) 

GO

ALTER TABLE [dbo].[tbProject_PostIt] 
  CHECK CONSTRAINT [FK_Ext_project_id] 

GO

ALTER TABLE [dbo].[tbProject_PostIt] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_postit_id] FOREIGN KEY([postit_id]) 
  REFERENCES [dbo].[tbPostIt] ([postit_id]) 

GO

ALTER TABLE [dbo].[tbProject_PostIt] 
  CHECK CONSTRAINT [FK_Ext_postit_id] 

GO

ALTER TABLE [dbo].[tbTeam_Project] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_users_id_T] FOREIGN KEY([users_id]) 
  REFERENCES [dbo].[tbUsers] ([users_id]) 

GO

ALTER TABLE [dbo].[tbTeam_Project] 
  CHECK CONSTRAINT [FK_Ext_users_id_T] 

GO

ALTER TABLE [dbo].[tbTeam_Project] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_project_id_T] FOREIGN KEY([project_id]) 
  REFERENCES [dbo].[tbProject] ([project_id]) 

GO

ALTER TABLE [dbo].[tbTeam_Project] 
  CHECK CONSTRAINT [FK_Ext_project_id_T] 

GO

ALTER TABLE [dbo].[tbUsers_Team] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_users_id_U] FOREIGN KEY([users_id]) 
  REFERENCES [dbo].[tbUsers] ([users_id]) 

GO

ALTER TABLE [dbo].[tbUsers_Team] 
  CHECK CONSTRAINT [FK_Ext_users_id_U] 

GO

ALTER TABLE [dbo].[tbUsers_Team] 
  WITH CHECK ADD CONSTRAINT [FK_Ext_team_id_U] FOREIGN KEY([team_id]) 
  REFERENCES [dbo].[tbTeam] ([team_id]) 

GO

ALTER TABLE [dbo].[tbUsers_Team] 
  CHECK CONSTRAINT [FK_Ext_team_id_U] 

GO

CREATE PROCEDURE spInsert_tbUsers
(
	@users_name VARCHAR(MAX),
	@users_picture VARBINARY(MAX),
	@users_nickname VARCHAR(20),
	@users_email VARCHAR(MAX),
	@users_password VARCHAR(MAX),
	@users_created_at DATE,
	@users_updated_at DATE
)
AS
BEGIN
	INSERT INTO tbUsers
		(users_name, users_picture, users_nickname, users_email, users_password, users_created_at, users_updated_at)
	VALUES
		(@users_name, @users_picture, @users_nickname, @users_email, @users_password, @users_created_at, @users_updated_at)
END
GO

CREATE PROCEDURE spInsert_tbTeam
(
	@team_picture VARBINARY(MAX),
	@team_name VARCHAR(MAX),
	@team_skill VARCHAR(50),
	@team_created_at DATE,
	@team_updated_at DATE
)
AS
BEGIN
	INSERT INTO tbTeam
		( team_picture, team_name, team_skill, team_created_at,team_updated_at)
	VALUES
		( @team_picture, @team_name, @team_skill, @team_created_at, @team_updated_at)
END
GO

CREATE PROCEDURE spInsert_tbProject
(
	@project_code VARCHAR(MAX),
	@project_picture VARBINARY(MAX),
	@project_description VARCHAR(50),
	@project_created_at DATE,
	@project_updated_at DATE
)
AS
BEGIN
	INSERT INTO tbProject
		( project_code, project_picture, project_description, project_created_at, project_updated_at)
	VALUES
		( @project_code, @project_picture, @project_description, @project_created_at, @project_updated_at)
END
GO

CREATE PROCEDURE spInsert_tbPostIt
(
	@postit_header VARCHAR(MAX),
	@postit_body VARCHAR(MAX),
	@postit_doing_by VARCHAR(50),
	@postit_status VARCHAR(50),
	@postit_created_at DATE,
	@postit_updated_at DATE

)
AS
BEGIN
	INSERT INTO tbPostIt
		( postit_header, postit_body, postit_doing_by, postit_status, postit_created_at, postit_updated_at)
	VALUES
		( @postit_header, @postit_body, @postit_doing_by, @postit_status, @postit_created_at, @postit_updated_at)
END
GO

CREATE PROCEDURE spUpdate_tbUsers
(
	@id INT,
	@users_picture VARBINARY(MAX),
	@users_nickname VARCHAR(20),
	@users_email VARCHAR(MAX),
	@users_password VARCHAR(MAX),
	@users_updated_at DATE
)
AS
BEGIN
	UPDATE tbUsers set
		users_picture = @users_picture,
		users_nickname = @users_nickname,
		users_email = @users_email,
		users_password = @users_password,
		users_updated_at = @users_updated_at
		WHERE users_id = @id

END
GO

CREATE PROCEDURE spUpdate_tbTeam
(
	@id INT,
	@team_picture VARBINARY(MAX),
	@team_name VARCHAR(MAX),
	@team_skill VARCHAR(50),
	@team_updated_at DATE
)
AS
BEGIN
	UPDATE tbTeam SET
		team_picture = @team_picture, 
		team_name = @team_name,
		team_skill = @team_skill,
		team_updated_at = @team_updated_at
		WHERE team_id = @id
END
GO

CREATE PROCEDURE spUpdate_tbProject
(
	@id INT,
	@project_picture VARBINARY(MAX),
	@project_description VARCHAR(50),
	@project_updated_at DATE
)
AS
BEGIN 
	UPDATE tbProject SET
		project_picture = @project_picture,
		project_description = @project_description,
		project_updated_at = @project_updated_at
		WHERE project_id = @id
		
END
GO

CREATE PROCEDURE spUpdate_tbPostIt
(
	@id INT,
	@postit_header VARCHAR(MAX),
	@postit_body VARCHAR(MAX),
	@postit_doing_by VARCHAR(50),
	@postit_status VARCHAR(50),
	@postit_updated_at DATE

)
AS
BEGIN
	UPDATE tbPostIt SET
		postit_header = @postit_header, 
		postit_body = @postit_body,
		postit_doing_by = @postit_doing_by,
		postit_status = @postit_status,
		postit_updated_at = @postit_updated_at
		WHERE postit_id = @id
END
GO

CREATE PROCEDURE spDelete
(
	@id INT,
	@id_name VARCHAR(MAX),
	@table VARCHAR(MAX)
)
AS
BEGIN
	DECLARE @sql VARCHAR(MAX);
	SET @sql = 'DELETE ' + @table +
		' WHERE ' + @id_name + ' = ' + CAST(@id AS VARCHAR(MAX))
	EXEC(@sql)
END
GO

CREATE PROCEDURE spQuery
(
	@id INT,
	@id_name VARCHAR(MAX),
	@table VARCHAR(MAX)
)
AS 
BEGIN
	DECLARE @sql VARCHAR(MAX)
	SET @sql = 'SELECT * FROM ' + @table + 
		' WHERE ' + @id_name + ' = ' + CAST(@id AS VARCHAR(MAX))
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

ALTER PROCEDURE spLogin
(
	@user	VARCHAR(MAX),
	@password	VARCHAR(MAX)
)
AS
BEGIN
	DECLARE @sql VARCHAR(MAX)
	
	SET @sql = CONCAT('SELECT * FROM tbUsers WHERE users_nickname = ', CHAR(39), CONVERT(VARCHAR,REPLACE(@user, CHAR(39), '')), CHAR(39),
						' AND users_password = ', CHAR(39), CONVERT(VARCHAR,REPLACE(@password, CHAR(39), '')), CHAR(39))

	EXEC(@sql)
END
GO

CREATE VIEW vwRetornaNick AS
SELECT US.users_nickname FROM tbUsers_Team UT
INNER JOIN tbUsers US ON US.users_id = UT.users_id
GO

CREATE VIEW vwRetornaTime AS
SELECT TE.team_name FROM tbUsers_Team UT
INNER JOIN tbTeam TE ON TE.team_id = UT.team_id
GO