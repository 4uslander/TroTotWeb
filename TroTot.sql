CREATE DATABASE TroTot
USE TroTot

CREATE TABLE [Role](
	roleId int primary key identity(1,1),
	roleName nvarchar(50)
)

CREATE TABLE Category(
	categoryId int primary key identity(1,1),
	categoryName nvarchar(50)
)

CREATE TABLE [Type](
	typeId int primary key identity(1,1),
	typeName nvarchar(50)
)

CREATE TABLE [User](
	userId int primary key identity(1,1),
	bio nvarchar(max),
	email nvarchar(50),
	[password] nvarchar(20),
	fullName nvarchar(50),
	[description] nvarchar(max),
	[image] nvarchar(max),
	phoneNumber nvarchar(20),
	dateOfBirth datetime,
	status int,
	roleId int references [Role](roleId)
)

CREATE TABLE [Order](
	orderId int primary key identity(1,1),
	userId int references [User](userId) ,
	qrimage nvarchar(max) not null,
	price money not null,
	note nvarchar(max),
	[dateTime] datetime not null,
	[status] int not null
)

CREATE TABLE Post(
	postId int primary key identity(1,1),
	title nvarchar(50),
	[image] nvarchar(max),
	[address] nvarchar(max),
	price money,
	[dateTime] datetime,
	area int,
	likeNumber int,
	comment int,
	contact nvarchar(20),
	status int,
	numberOfPeople int,
	categoryId int references Category(categoryId),
	typeId int references [Type](typeId),
	userId int references [User](userId)
)

CREATE TABLE Blog(
	blogId int primary key identity(1,1),
	title nvarchar(50),
	[description] nvarchar(max),
	[dateTime] datetime,
	status int,
	userId int references [User](userId)
)

INSERT INTO [Role](roleName) VALUES('User')
INSERT INTO [Role](roleName) VALUES('Admin')

INSERT INTO Category(categoryName) VALUES(N'Tìm trọ')
INSERT INTO Category(categoryName) VALUES(N'Tìm bạn ở ghép')

INSERT INTO [Type](typeName) VALUES (N'Phòng đơn')
INSERT INTO [Type](typeName) VALUES (N'Phòng đôi')

INSERT INTO [User](bio, email, [password], fullName, [description], [image], phoneNumber, dateOfBirth, status, roleId)
VALUES(N'Chào các bạn, mình tên là Quyên, mình 21 tuổi.', 'quyen@gmail.com', '1', N'Phan Thị Tố Quyên', N'Không biết mô tả gì!', null,
'0123456789', CAST(N'2023-04-26T00:00:00.000' AS DateTime), 1, 1)

INSERT INTO Post(title, [image], [address], price, [dateTime], area, likeNumber, comment, contact, status, numberOfPeople, categoryId,
typeId, userId) VALUES (N'Tìm bạn ở ghép', null, N'Hồ Chí Minh', 100000, CAST(N'2023-04-26T00:00:00.000' AS DateTime), 100, 10, 11, '0123456789',
1, 1, 1, 1, 1)

