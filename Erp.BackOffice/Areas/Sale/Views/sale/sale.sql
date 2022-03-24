create table sale
(
	Id						int identity
	, IsDeleted				bit default(0)
	, CreatedDate			datetime default getdate()
	, CreatedUserId			int
	, ModifiedDate			datetime default getdate()
	, ModifiedUserId		int
	, AssignedUserId		int
	
	, Name					nvarchar(150)
	, [Product_id] int
	, [Product_idsame] int

		
	Primary Key(Id)
)

-- lấy id của dòng trên sau khi Execute điền vào cột [ParentId]
INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('sale_sale_Index', N'sale', N'Index', N'sale', NULL, 1, 1, NULL
           , 0, 1, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('sale_sale_Create', N'sale', N'Create', N'sale', NULL, 2, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('sale_sale_Edit', N'sale', N'Edit', N'sale', NULL, 3, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);
		   
INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('sale_sale_Detail', N'sale', N'Detail', N'sale', NULL, 3, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);

INSERT INTO [dbo].[System_Page]
           ([Name],[AreaName],[ActionName],[ControllerName],[Url],[OrderNo],[Status],[CssClassIcon]
           ,[IsDeleted],[IsVisible],[IsView],[IsAdd],[IsEdit],[IsDelete],[IsImport],[IsExport],[IsPrint])
     VALUES
           ('sale_sale_Delete', N'sale', N'Delete', N'sale', NULL, 4, 1, NULL
           , 0, 0, 0, 0, 0, 0, 0, 0, 0);
GO

---- sau khi chạy 5 dòng trên thì lấy id tương ứng của 5 dòng đó điền vào cột [System_PageId]
---------------------------------------------------------
INSERT INTO [dbo].[System_PageMenu]
           ([LanguageId],[PageId],[Name])
     VALUES
           ('vi-VN', (select Id from [System_Page] where [ActionName] = N'Index' and [ControllerName] = N'sale'), N'sale')
GO

SELECT TOP 5 *
  FROM [dbo].[System_Page]
  order by id desc

SELECT TOP 1 [LanguageId], Name
  FROM [dbo].[System_PageMenu]
  order by id desc