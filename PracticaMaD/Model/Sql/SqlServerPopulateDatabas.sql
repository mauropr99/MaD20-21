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

INSERT INTO Language (name, country) VALUES ('es', 'ES');
INSERT INTO Language (name, country) VALUES ('gl', 'ES');
INSERT INTO Language (name, country) VALUES ('en', 'US');
