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


/* LANGUAGE */
INSERT INTO Language (name, country) VALUES ('es', 'ES');
INSERT INTO Language (name, country) VALUES ('gl', 'ES');
INSERT INTO Language (name, country) VALUES ('en', 'US');
INSERT INTO Language (name, country) VALUES ('en', 'GB');

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
INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Edicion Bolsillo El Quijote', 30.6, '20190618 10:34:09 AM', 100, 2) 
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (8, 'El Quijote', 'Aventura', 'ALFAGUARA', '978-84-204-3547-3') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Libro científico de Stephen Hawking', 23.6, '20190618 10:34:09 AM', 120, 2) 
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (9, 'BREVES RESPUESTAS A LAS GRANDES PREGUNTAS', 'Ciencia', 'John Murray', '476-44-234-9876-6') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Libros de la colección Geronimo Stilton', 15.3, '20030618 10:34:09 AM', 12, 2)
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (10, 'El reino de la fantasía', 'Literatura Juvenil', 'Planeta', '212-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Libros de la colección Geronimo Stilton', 15.3, '20030618 10:34:09 AM', 23, 2)
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (11, 'Segundo viaje al reino de la fantasía', 'Literatura Juvenil', 'Planeta', '213-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Libros de la colección Geronimo Stilton', 15.3, '20030618 10:34:09 AM', 20, 2)
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (12, 'Tercer viaje al reino de la fantasía', 'Literatura Juvenil', 'Planeta', '214-0-222-26421-4')

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Trilogía El Señor de los Anillos', 22.6, '20190618 10:34:09 AM', 34, 2) 
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (13, 'El Retorno del Rey', 'Ciencia Ficción', 'Minotauro', '111-23-205-3557-1') 

INSERT INTO Product (product_name, price, releaseDate, stock, categoryId) VALUES ('Trilogía El Señor de los Anillos', 21.6, '20190618 10:34:09 AM', 66, 2) 
INSERT INTO Book (id, title, genre, editorial, isbnCode) VALUES (14, 'La Comunidad del Anillo', 'Ciencia Ficción', 'Minotauro', '112-23-205-3557-2') 

