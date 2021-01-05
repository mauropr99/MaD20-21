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

DELETE FROM Comment			DBCC CHECKIDENT ('[Comment]', RESEED, 0);
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

/* USER */
	
INSERT INTO User_Table (login, name, lastName, password, email, languageId, role, favouriteCreditCard) VALUES ('admin', 'admin', 'admin', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'admin@gmail.com', 1, 'admin', NULL);
INSERT INTO User_Table (login, name, lastName, password, email, languageId, role, favouriteCreditCard) VALUES ('user', 'user', 'user', 'BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=', 'user@user.com', 1, 'user', NULL);
INSERT INTO User_Table (login, name, lastName, password, email, languageId, role, favouriteCreditCard) VALUES ('eloy', 'eloy', 'eloy', 'wkm19O5dCgD0zZP8Ybb7+nfI/wGlM7peknzSdSngxQs=', 'eloy@eloy.es', 1, 'user', NULL);

/* CREDITCARD */
INSERT INTO CreditCard (ownerName, creditType, creditCardNumber,cvv, expirationDate) VALUES(user,'debit','0123456789123456',123,'20240601 00:00:00 AM')

/* CREDITCARD_USER */
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
INSERT INTO Order_Table (postalAddress, orderDate, totalPrice, userId, creditCardId, description) VALUES ('Calle inventada Nº1', '20201229 00:00:00 AM', 2533.1, 2, 1, 'Pedido variado de 5 elementos');

/* ORDER_LINE */
INSERT INTO OrderLine (quantity, price, productId, orderId) VALUES (1, 999.3, 1, 1)
INSERT INTO OrderLine (quantity, price, productId, orderId) VALUES (2, 700.4, 4, 1)
INSERT INTO OrderLine (quantity, price, productId, orderId) VALUES (1, 30.6, 8, 1)
INSERT INTO OrderLine (quantity, price, productId, orderId) VALUES (3, 23.6, 9, 1)
INSERT INTO OrderLine (quantity, price, productId, orderId) VALUES (2, 15.3, 10, 1)

INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (1, 15,'Este último libro cierra magistralmente la saga de El Señor de los Anillos y el mundo Tolkien. Es asombroso todo lo que JRR Tolkien podía tener en su mente para imaginar un universo tan vasto, lo que significa crear un lenguaje imaginarse muchos otros, crear varios mundos a lo largo de más de 4000 años, mapas, geografías enteras, historia, árboles genealógicos, culturas, en fin. ¡Increíble!','20200314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 15,'Interesting to get these books in different languages!','20190314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 15,'18 páginas faltantes, una página ilegible, por error de impresión, lástima tendré que buscar por otro lado las 18 páginas perdidas.','20180314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 15,'Pasé momentos muy entretenidos durante la lectura de este gran libro. Totalmente recomendado. Me gusta la posibilidad que tiene la aplicación para escuchar la lectura, aunque sea un poco robótica.','20181214 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (3, 15,'Un emocionante y conmovedor desenlace para la mejor saga fantástica que he leído hasta el momento.','20201214 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (3, 15,'El libro llegó en excelentes condiciones','20180314 00:00:00 AM')


INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (1, 8,'Gran libro del Quijote en un castellano en el que la gente de hoy en día podría entender sin tener que estrujarse la cabeza a la hora de entender el español de hace más de 400 años. Recomendado para la gente que quiera leer el Quijote entendiéndolo todo sin problemas. La presentacion de esta edición en tapa dura, forrada en tela azul oscuro, marcapaginas de tela azul también, con sobrecubierta de papel. Las páginas son finas, tipo biblia. Muy recomendable. Imprenscidible para los amantes del Quijote.','20200314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 8,'Trabajo colosal de mucho años por parte de Andrés Trapiello que agradecerán muchos millones de lectores en España, América y resto del mundo. Tal vez la traducción al castellano contemporáneo pierda algo de la esencia original de la obra pero permite la lectura completa del libro en mucho menos tiempo. Algo nada desdeñable en un mundo en que el paso del tiempo se mide ya no en minutos sino en nanosegundos. Los que prefieran el texto original, tienen por supuesto la opción de leer la versión de Cervantes.','20190314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 8,'El problema es que está mezclados los comentarios de varias ediciones incluida la del Kindle, por lo tanto te puede pasar como a mi, que compres una edición que crees que tiene notas al pie y no las tiene.Devolución en toda regla y buscar comentarios y opiniones fuera de amazon','20180314 00:00:00 AM')


INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (1, 4,'Torre pequeña, ocupa poco espacio y no pesa nada. En lo que se refiere al rendimiento va muy fluido y rapido gracias al sdd que lleva. Para trabajos de oficina o uso diario esta genial.','20200314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 4,'Muy buena pantalla!! Rápido, buena batería Diseño bonito y cámaras eficientes. Pensaba que pesaría más pero no..','20190314 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 4,'Por decir algo, diria que no lleva puerto VGA y eso hay que tenerlo en cuenta porque si tenemos un monitor sin entrada HDMI, se necesitara un adaptador. Pero bueno, cada vez es mas frecuente que los fabricantes eliminen el puerto VGA al igual que el almacenamiento optico.','20180314 00:00:00 AM')


INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (2, 10,'Una vergüenza. El libro está destrozado. Como puede anunciarse como “buen” estado?','20181214 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (3, 10,'Me gusta mucho para iniciar al peque en los libros. Es algo a medio camino entre los cuentos y los libros, porque el formato es de libro pero el contenido aún se asemeja a lo interactivo de un cuento y un juego: hay olores, acertijos, grafías divertidas, y capítulos cortos que animan a avanzar. Estoy contenta con ellos, veo que se divierte leyendo a pesar de que no es su afición preferida...','20201214 00:00:00 AM')
INSERT INTO Comment (userId, productId, text, commentDate ) VALUES (3, 10,'Por sacarle alguna pega: los olores a veces son tan “plastificados” que cuesta diferenciar los unos de los otros. Aún así la idea me encanta, pero en mi opinión este aspecto es a mejorar','20180314 00:00:00 AM')

