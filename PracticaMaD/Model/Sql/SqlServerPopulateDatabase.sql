/* 
 * SQL Server Script
 * 
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'
 
 
USE practicamad

DELETE FROM OrderLine		DBCC CHECKIDENT ('[OrderLine]', RESEED, 0);
DELETE FROM Order_Table		DBCC CHECKIDENT ('[Order_Table]', RESEED, 0);
DELETE FROM Book;
DELETE FROM Computers; 
DELETE FROM Product			DBCC CHECKIDENT ('[Product]', RESEED, 0);
DELETE FROM CreditCard_User;
DELETE FROM CreditCard		DBCC CHECKIDENT ('[CreditCard]', RESEED, 0);
DELETE FROM User_Table		DBCC CHECKIDENT ('[User_Table]', RESEED, 0);
DELETE FROM Category		DBCC CHECKIDENT ('[Category]', RESEED, 0);
DELETE FROM Language		DBCC CHECKIDENT ('[Language]', RESEED, 0);




/* LANGUAGE */
INSERT INTO Language (name, country) VALUES ('es', 'ES');
INSERT INTO Language (name, country) VALUES ('gl', 'ES');
INSERT INTO Language (name, country) VALUES ('en', 'US');
INSERT INTO Language (name, country) VALUES ('en', 'GB');

/* User */
	
INSERT INTO User_Table (login, name, lastName, password, email, languageId, role, favouriteCreditCard) VALUES ('admin', 'admin', 'admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'admin@gmail.com', 1, 'admin', NULL);
INSERT INTO User_Table (login, name, lastName, password, email, languageId, role, favouriteCreditCard) VALUES ('user', 'user', 'user', 'BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=', 'user@user.com', 1, 'user', NULL);

/* CreditCard */
INSERT INTO CreditCard (ownerName, creditType, creditCardNumber,cvv, expirationDate) VALUES(user,'debit','012345678912345',123,'20240618 00:00:00 AM')

/* CreditCard_User */
INSERT INTO CreditCard_User(userId, creditCardId)  VALUES (2,1);

/* CATEGORY */
INSERT INTO Category (name) VALUES ('Computers');
INSERT INTO Category (name) VALUES ('Books');

/* COMPUTERS */
INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Portatil Acer SSD', 1000.3, '20190618 10:34:09 AM', 20, 1)
INSERT INTO Computers (id, brand, processor, os) VALUES(1, 'Acer', 'Intel i7', 'Windows 10 Home')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('MacBook Pro', 1120.7, '20180618 12:44:09 AM', 30, 1)
INSERT INTO Computers (id, brand, processor, os) VALUES(2, 'Apple', 'Intel i7', 'macOS')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Portatil Ultrabook', 940.6, '20190618 10:34:09 AM', 26, 1)
INSERT INTO Computers (id, brand, processor, os) VALUES(3, 'Lenovo', 'AMD Ryzen 5', 'Windows 10 Pro')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('All-In-One Acer Gaming', 700.4, '20190618 12:14:09 AM', 12, 1)
INSERT INTO Computers (id, brand, processor, os) VALUES(4, 'Acer', 'Intel i5', 'Windows 10 Home')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Portatil MSI 6QD', 800.50, '20190618 10:34:09 AM', 20, 1)  
INSERT INTO Computers (id, brand,processor, os) VALUES(5,'Msi','Intel i7','Windows 7')  

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Portatil Lenovo thinkpad', 759.50, '20190618 10:34:09 AM', 200, 1)  
INSERT INTO Computers (id, brand,processor, os) VALUES(6,'Lenovo','Intel i3','FreeOs')  

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Portatil HP Pavilon', 899.50, '20190618 10:34:09 AM', 800, 1)
INSERT INTO Computers (id, brand,processor, os) VALUES(7,'HP','AMD Ryzen 7','Ubuntu 20.04')  
 
/* BOOKS */
INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('El Quijote', 30.6, '20190618 10:34:09 AM', 100, 2) 
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (8, 'Miguel de Cervantes', 'Aventura', 'ALFAGUARA', '978-84-204-3547-3') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Brief answers to the big questions', 23.6, '20190618 10:34:09 AM', 120, 2) 
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (9, 'Stephen Hawking', 'Ciencia', 'John Murray', '476-44-234-9876-6') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('El reino de la fantasía', 15.3, '20030618 10:34:09 AM', 12, 2)
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (10, 'Geronimo Stilton', 'Literatura Juvenil', 'Planeta', '212-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Segundo viaje al reino de la fantasía ', 15.3, '20030618 10:34:09 AM', 23, 2)
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (11, 'Geronimo Stilton', 'Literatura Juvenil', 'Planeta', '213-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Tercer viaje al reino de la fantasía', 15.3, '20030618 10:34:09 AM', 8, 2)
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (12, 'Geronimo Stilton', 'Literatura Juvenil', 'Planeta', '214-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('El Señor de los Anillos: La Comunidad del Anillo', 21.6, '20100618 10:34:09 AM', 10, 2) 
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (13, 'J.R.R.Tolkien', 'Ciencia Ficción', 'Minotauro', '112-23-205-3557-2') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('El Señor de los Anillos: Las Dos Torres', 21.6, '20110618 10:34:09 AM', 7, 2) 
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (14, 'J.R.R.Tolkien', 'Ciencia Ficción', 'Minotauro', '112-23-205-3557-2') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('El Señor de los Anillos: El Retorno del Rey', 22.6, '20120618 10:34:09 AM', 4, 2) 
INSERT INTO Book (id, author, genre, editorial, isbnCode) VALUES (15, 'J.R.R.Tolkien', 'Ciencia Ficción', 'Minotauro', '111-23-205-3557-1') 



/* ORDERS */
INSERT INTO Order_Table(postalAddress, orderDate, totalPrice, userId, creditCardId, description) VALUES ('Calle inventada Nº1', '20201229 00:00:00 AM', 20, 1, 1, 'Descripcion del pedido');

/* ORDER_LINE */
INSERT INTO OrderLine(quantity, price, productId, orderId) VALUES(1, 0, 2, 1)
