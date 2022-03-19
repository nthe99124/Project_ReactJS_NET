IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [BillStatus] (
    [Id] int NOT NULL IDENTITY,
    [StatusDescription] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_BillStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Color] (
    [Id] int NOT NULL IDENTITY,
    [ColorName] nvarchar(max) NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Color] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Image] (
    [Id] bigint NOT NULL IDENTITY,
    [UrlImage] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [News] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Content] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_News] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [RoleName] nvarchar(max) NULL,
    [RoleDescription] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [User] (
    [Id] bigint NOT NULL IDENTITY,
    [UserName] nvarchar(max) NOT NULL,
    [PassWord] nvarchar(max) NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [DoB] datetime2 NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Brand] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [ImageID] bigint NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Brand_Image_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Image] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [NewsImage] (
    [Id] bigint NOT NULL IDENTITY,
    [NewsID] bigint NOT NULL,
    [ImageID] bigint NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_NewsImage] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_NewsImage_Image_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Image] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NewsImage_News_NewsID] FOREIGN KEY ([NewsID]) REFERENCES [News] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bill] (
    [Id] bigint NOT NULL IDENTITY,
    [CustomerID] bigint NOT NULL,
    [UserId] bigint NULL,
    [DateOrder] datetime2 NOT NULL,
    [Address] nvarchar(max) NULL,
    [StatusID] int NOT NULL,
    [BillStatusId] int NULL,
    [Phone] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Bill] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bill_BillStatus_BillStatusId] FOREIGN KEY ([BillStatusId]) REFERENCES [BillStatus] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Bill_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [UserRole] (
    [Id] bigint NOT NULL IDENTITY,
    [UserID] bigint NOT NULL,
    [RoleID] int NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserRole_Role_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Product] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [BrandID] int NULL,
    [Description] nvarchar(max) NULL,
    [Price] decimal(18,0) NOT NULL,
    [PromotionPrice] decimal(18,0) NOT NULL,
    [Option] nvarchar(max) NULL,
    [Type] int NULL,
    [Warranty] decimal(18,0) NOT NULL,
    [Weight] decimal(18,0) NOT NULL,
    [Size] nvarchar(max) NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Brand_BrandID] FOREIGN KEY ([BrandID]) REFERENCES [Brand] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Cart] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NOT NULL,
    [CustomerId] bigint NOT NULL,
    [UserId] bigint NULL,
    [QuantityPurschased] int NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cart_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Cart_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [FavoriteList] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductID] bigint NOT NULL,
    [CustomerID] bigint NOT NULL,
    [UserId] bigint NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_FavoriteList] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FavoriteList_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FavoriteList_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProductColor] (
    [ProductID] bigint NOT NULL,
    [ColorID] int NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_ProductColor] PRIMARY KEY ([ColorID], [ProductID]),
    CONSTRAINT [FK_ProductColor_Color_ColorID] FOREIGN KEY ([ColorID]) REFERENCES [Color] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductColor_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ProductImage] (
    [ProductID] bigint NOT NULL,
    [ImageID] bigint NOT NULL,
    [CreatedOn] DateTime NOT NULL,
    [CreatedBy] bigint NOT NULL,
    [UpdatedOn] DateTime NOT NULL,
    [UpdatedBy] bigint NOT NULL,
    CONSTRAINT [PK_ProductImage] PRIMARY KEY ([ImageID], [ProductID]),
    CONSTRAINT [FK_ProductImage_Image_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Image] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductImage_Product_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Bill_BillStatusId] ON [Bill] ([BillStatusId]);
GO

CREATE INDEX [IX_Bill_DateOrder] ON [Bill] ([DateOrder]);
GO

CREATE INDEX [IX_Bill_UserId] ON [Bill] ([UserId]);
GO

CREATE INDEX [IX_Brand_ImageID] ON [Brand] ([ImageID]);
GO

CREATE INDEX [IX_Cart_ProductId] ON [Cart] ([ProductId]);
GO

CREATE INDEX [IX_Cart_UserId] ON [Cart] ([UserId]);
GO

CREATE INDEX [IX_FavoriteList_ProductID] ON [FavoriteList] ([ProductID]);
GO

CREATE INDEX [IX_FavoriteList_UserId] ON [FavoriteList] ([UserId]);
GO

CREATE INDEX [IX_NewsImage_ImageID] ON [NewsImage] ([ImageID]);
GO

CREATE INDEX [IX_NewsImage_NewsID] ON [NewsImage] ([NewsID]);
GO

CREATE INDEX [IX_Product_BrandID] ON [Product] ([BrandID]);
GO

CREATE INDEX [IX_ProductColor_ProductID] ON [ProductColor] ([ProductID]);
GO

CREATE INDEX [IX_ProductImage_ProductID] ON [ProductImage] ([ProductID]);
GO

CREATE INDEX [IX_UserRole_RoleID] ON [UserRole] ([RoleID]);
GO

CREATE INDEX [IX_UserRole_UserID] ON [UserRole] ([UserID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220318033551_First', N'5.0.10');
GO

COMMIT;
GO

