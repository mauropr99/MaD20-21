/* 
 * SQL Server Script
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */



/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE practicamad


/* Drop Table OrderLine if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[OrderLine]') 
AND type in ('U')) DROP TABLE [OrderLine]
GO

/* Drop Table Order_Table if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Order_Table]') 
AND type in ('U')) DROP TABLE [Order_Table]
GO


/* Drop Table Label_Comment if already exists */


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Label_Comment]') 
AND type in ('U')) DROP TABLE [Label_Comment]
GO

/* Drop Table Product if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') 
AND type in ('U')) DROP TABLE [Comment]
GO

/* Drop Table Product if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Label]') 
AND type in ('U')) DROP TABLE [Label]
GO

/* Drop Table Laptop if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Laptop]') 
AND type in ('U')) DROP TABLE [Laptop]
GO

/* Drop Table Desktop if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Desktop]') 
AND type in ('U')) DROP TABLE [Desktop]
GO

/* Drop Table Computer if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Computers]') 
AND type in ('U')) DROP TABLE [Computers]
GO

/* Drop Table Book if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Book]') 
AND type in ('U')) DROP TABLE [Book]
GO

/* Drop Table Product if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Product]') 
AND type in ('U')) DROP TABLE [Product]
GO

/* Drop Table Category if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE [Category]
GO

/* Drop Table CreditCard_User if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CreditCard_User]') 
AND type in ('U')) DROP TABLE [CreditCard_User]
GO

/* Drop Table User_Table if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[User_Table]') 
AND type in ('U')) DROP TABLE [User_Table]
GO

/* Drop Table CreditCard if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[CreditCard]') 
AND type in ('U')) DROP TABLE [CreditCard]
GO

/* Drop Table Language if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Language]') 
AND type in ('U')) DROP TABLE [Language]
GO


/*
 * Create tables.
 * Tables are created. Indexes required for the 
 * most common operations are also defined.
 */

/* Language */

CREATE TABLE Language (
	id bigint IDENTITY(1,1) NOT NULL, 
	name VARCHAR(64) NOT NULL,
	country VARCHAR(64) NOT NULL,
    
    CONSTRAINT [PK_Language] PRIMARY KEY (id ASC)
    
)

CREATE NONCLUSTERED INDEX IX_LanguageIndexById 
ON Language (id);

PRINT N'Table Language created.'
GO

PRINT N'Done'

/* CreditCard */

CREATE TABLE CreditCard (
	id bigint IDENTITY(1,1) NOT NULL,
	ownerName VARCHAR(128) NOT NULL,
	creditType VARCHAR(30) NOT NULL CHECK(creditType IN ('debit','credit')),
	creditCardNumber VARCHAR (16) NOT NULL,
	cvv smallint NOT NULL,
	expirationDate date NOT NULL,
    
    CONSTRAINT [PK_CreditCard] PRIMARY KEY (id ASC)
    
)


CREATE NONCLUSTERED INDEX IX_CreditCardIndexById 
ON CreditCard (id);

PRINT N'Table CreditCard created.'
GO

PRINT N'Done'

/* User */

CREATE TABLE User_Table (
	id bigint IDENTITY(1,1) NOT NULL,
	login VARCHAR(64) NOT NULL,
	name VARCHAR(64) NOT NULL,
	lastName VARCHAR(64) NOT NULL,
	password VARCHAR(64) NOT NULL,
	address VARCHAR(64) NOT NULL,
	email VARCHAR(64) NOT NULL,
	languageId BIGINT NOT NULL,
	role VARCHAR(30) NOT NULL CHECK(role IN ('admin','user')),

    CONSTRAINT [PK_User] PRIMARY KEY (id ASC),

	CONSTRAINT [FK_LanguageId] FOREIGN KEY(languageId)
        REFERENCES Language (id) ON DELETE NO ACTION,
) 

CREATE NONCLUSTERED INDEX [IX_UserIndexById] 
ON User_Table (id ASC)

/*TODO mirar los índices*/


PRINT N'Table User created.'
GO

PRINT N'Done'


/* CreditCard_User */

CREATE TABLE CreditCard_User (
	userId bigint NOT NULL, 
	creditCardId bigint NOT NULL, 
    
    CONSTRAINT [PK_CreditCard_User] PRIMARY KEY (userId, creditCardId),
	
	CONSTRAINT [FK_CreditCard_User_User] FOREIGN KEY(userId)
        REFERENCES User_Table (id) ON DELETE CASCADE,
	
	CONSTRAINT [FK_CreditCard_User_CreditCard] FOREIGN KEY(creditCardId)
        REFERENCES CreditCard (id) ON DELETE CASCADE
	
)

PRINT N'Table CreditCard_User created.'
GO

PRINT N'Done'


/* Category */

CREATE TABLE Category (
	id bigint IDENTITY(1,1) NOT NULL, 
	name VARCHAR(64) NOT NULL,

    CONSTRAINT [PK_Category] PRIMARY KEY (id ASC),

)


CREATE NONCLUSTERED INDEX IX_CategoryIndexById 
ON Category (id);

PRINT N'Table Desktop created.'
GO


/* Product */

CREATE TABLE Product (
	id BIGINT IDENTITY(1,1) NOT NULL, 
	product_name VARCHAR(64) NOT NULL,
	price DECIMAL(9,5) NOT NULL,
	releaseDate DATE NOT NULL,
	stock INT NOT NULL,
	categoryId BIGINT NOT NULL,
    
    CONSTRAINT [PK_Product] PRIMARY KEY (id ASC),

	
    CONSTRAINT [FK_ProductCategoryId] FOREIGN KEY(categoryId)
        REFERENCES Category (id) ON DELETE NO ACTION
)


CREATE NONCLUSTERED INDEX IX_ProductIndexById 
ON Product (id);

PRINT N'Table Product created.'
GO

PRINT N'Done'

/* Computers */

CREATE TABLE Computers (
	id bigint NOT NULL, 
	brand VARCHAR(64) NOT NULL,
	processor VARCHAR(64),
	os VARCHAR(64),



    CONSTRAINT [PK_Computers] PRIMARY KEY (id ASC),

	CONSTRAINT [FK_Computers] FOREIGN KEY(id)
        REFERENCES Product (id) ON DELETE CASCADE
	
)


CREATE NONCLUSTERED INDEX IX_ComputersIndexById 
ON Computers (id);

PRINT N'Table Computers created.'
GO
PRINT N'Done'


/* Desktop 

CREATE TABLE Desktop (
	id bigint NOT NULL, 
	allInOne BIT NOT NULL,


    CONSTRAINT [PK_Desktop] PRIMARY KEY (id ASC),

	CONSTRAINT [FK_DesktopComputer] FOREIGN KEY(id)
        REFERENCES Computers (id) ON DELETE CASCADE

)


CREATE NONCLUSTERED INDEX IX_DesktopIndexById 
ON Desktop (id);

PRINT N'Table Desktop created.'
GO
*/

/* Laptop 

CREATE TABLE Laptop (
	id bigint NOT NULL, 
	screenResolution VARCHAR(64),
	screenInches SMALLINT,

    CONSTRAINT [PK_Laptop] PRIMARY KEY (id ASC),

	CONSTRAINT [FK_LaptopComputer] FOREIGN KEY(id)
        REFERENCES Computers (id) ON DELETE CASCADE

)


CREATE NONCLUSTERED INDEX IX_LaptopIndexById 
ON Laptop (id);

PRINT N'Table Laptop created.'
GO

*/
/* Book */

CREATE TABLE Book (
	id bigint NOT NULL, 
	title VARCHAR(64) NOT NULL,
	genre VARCHAR(64) NOT NULL,
	editorial VARCHAR(64) NOT NULL,
	isbnCode VARCHAR(64) NOT NULL,

    CONSTRAINT [PK_Book] PRIMARY KEY (id ASC),

    CONSTRAINT [FK_BookId] FOREIGN KEY(id)
        REFERENCES Product (id) ON DELETE CASCADE
	
)


CREATE NONCLUSTERED INDEX IX_BookIndexById 
ON Book (id);

PRINT N'Table Book created.'
GO


/* Label */

CREATE TABLE Label (
	id bigint IDENTITY(1,1) NOT NULL, 
	lab VARCHAR(64) NOT NULL,
	timesUsed int,

    CONSTRAINT [PK_Label] PRIMARY KEY (id ASC),
)

CREATE NONCLUSTERED INDEX IX_LabelIndexById 
ON Label (id);

PRINT N'Table Label created.'
GO

PRINT N'Done'

/* Comment */

CREATE TABLE Comment (
	id BIGINT IDENTITY(1,1) NOT NULL, 
	userId BIGINT  NOT NULL,
	productId BIGINT NOT NULL,
	text VARCHAR(640) NOT NULL,
	commentDate date NOT NULL,
    
    CONSTRAINT [PK_Comment] PRIMARY KEY (id ASC),

	CONSTRAINT [FK_Comment_UserId] FOREIGN KEY(userId)
		REFERENCES User_Table (id) ON DELETE NO ACTION,

	CONSTRAINT [FK_Comment_ProductId] FOREIGN KEY(productId)
		REFERENCES Product (id) ON DELETE NO ACTION,
)


CREATE NONCLUSTERED INDEX IX_CommentIndexById 
ON Comment (id);

PRINT N'Table Comment created.'
GO

PRINT N'Done'

/* Label_Comment */

CREATE TABLE Label_Comment (
	commentId bigint NOT NULL,
	labId bigint NOT NULL,

    CONSTRAINT [PK_Label_Comment] PRIMARY KEY (commentId, labId),

	CONSTRAINT [FK_Label_Comment_CommentId] FOREIGN KEY(commentId)
		REFERENCES Comment (id) ON DELETE CASCADE,

	CONSTRAINT [FK_Label_Comment_LabelId] FOREIGN KEY(labId)
		REFERENCES Label (id) ON DELETE CASCADE
)

PRINT N'Table  Label_Comment created.'
GO

PRINT N'Done'

/* Order */

CREATE TABLE Order_Table (
	id bigint IDENTITY(1,1) NOT NULL, 
	postalAddress VARCHAR (200) NOT NULL,
	orderDate date NOT NULL,
	totalPrice DECIMAL(9,5) NOT NULL,
	userId bigint NOT NULL,
	creditCardId bigint NOT NULL,
	description VARCHAR(200) NOT NULL,
    
    CONSTRAINT [PK_Order] PRIMARY KEY (id ASC),
    CONSTRAINT [FK_UserId] FOREIGN KEY(userId)
        REFERENCES User_Table (id) ON DELETE CASCADE,
	CONSTRAINT [FK_Order_CreditCardId] FOREIGN KEY(creditCardId)
        REFERENCES CreditCard (id) ON DELETE CASCADE
)


CREATE NONCLUSTERED INDEX IX_OrderIndexById 
ON Order_Table (id);

PRINT N'Table Order created.'
GO

PRINT N'Done'



/* OrderLine */

CREATE TABLE OrderLine (
	id bigint IDENTITY(1,1) NOT NULL, 
	quantity smallint NOT NULL,
	price DECIMAL(9,5) NOT NULL,
   	productId BIGINT NOT NULL,
	orderId BIGINT,

    CONSTRAINT [PK_OrderLine] PRIMARY KEY (id ASC),

    CONSTRAINT [FK_ProductId] FOREIGN KEY(productId)
        REFERENCES Product (id) ON DELETE NO ACTION,
	CONSTRAINT [FK_OrderId] FOREIGN KEY(orderId)
        REFERENCES Order_Table (id) ON DELETE CASCADE
	
)


CREATE NONCLUSTERED INDEX IX_OrderIndexById 
ON OrderLine (id);

PRINT N'Table OrderLine created.'
GO

PRINT N'Done'


