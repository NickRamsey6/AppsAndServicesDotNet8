# Chapter 2 Managing Relational Data Using SQL Server

## Key Concepts
* Connect to an existing SQL Server database
* Execute a simple query and process the results using fast and low-level ADO.NET
* Execute a simple query and process the results using Dapper

## SQL Server Data Types
| Category | Examples |
|----------|----------|
| Numbers | bigint, bit, decimal, float, int, money, numeric, real, smallint, smallmoney, tinyint |
| Date and time | date, datetime2, datetime, datetimeoffset, smalldatetime, time |
| Text | char, nchar, ntext, nvarchar, text, varchar |
| Binary | binary, image, varbinary | 
| Other | cursor, hierarchyid, sql_variant, table, rowversion, uniqueidentifier, xml |

## Example SELECT Statements
| Example | Description |
|---------|-------------|
| SELECT * FROM Employees | Get all columns of all the employees. |
| SELECT FirstName, LastName FROM Employees | Get the first and last name columns of all employees. |
| SELECT emp.FirstName, emp.LastName FROM Employees AS emp | Give an alias for the table name. Table name prefixes are not needed when there is only one table, but become useful to disambiguate when there are multiple tables that have columns with the same name. |
| SELECT emp.FirstName, emp.LastName FROM Employees emp | Give an alias for the table name without needing the AS keyword. |
| SELECT FirstName, LastName AS Surname FROM Employees | Give an alias for the column name. |
| SELECT FirstName, LastName FROM Employees WHERE Country = 'USA' | Filter the results to only include employees in the USA. |
| SELECT DISTINCT Country FROM Employees | Get a list if countries used as values in the Country column of the Employees table without duplicates. |
| SELECT UnitPrice * Quantity AS Subtotal FROM [Order Details] | Calculate a subtotal for each order detail row. |
| SELECT OrderId, SUM(UnitPrice * Quantity) AS Total FROM [Order Details] GROUP BY OrderId ORDER BY Total DESC | Calculate a total for each order and sort with the largest order value at the top. |
| SELECT CompanyName FROM Customers UNION SELECT CompanyName FROM Suppliers | Return all the company names of all customers and suppliers. |
| SELECT CategoryName, ProductName FROM Categories, Products | Match *every* category with *every* product using a Cartesian join and output their names (not what you normally want!). 616 rows (8 categories X 77 products). |
| SELECT CategoryName, ProductName FROM Categories c, Products p WHERE C.CategoryId = p.CategoryId | Match each product with its category using a WHERE clause for the **CategoryId** column in each table and output the category name and product name. 77 rows. |
| SELECT CategoryName, ProductName FROM Categories c INNER JOIN Products p ON c.CategoryId = p.CategoryId | Match each product with its category using an **INNER JOIN...ON** clause for the CategoryId column in each table and output the category name and product name. This is a modern alternative syntax to using WHERE, and it allows outer joins which would also include non-matches. 77 rows. |

## Example DML Statements
| Example | Description |
|---------|-------------|
| INSERT Employees(FirstName, LastName) VALUES('Mark', 'Price') | Add a new row to the Employees table. The EmployeeId primary key value is automatically assigned. Use @@IDENTITY to get this value. |
| UPDATE Employees SET Country = 'UK' WHERE FirstName = 'Mark' and LastName = 'Price' | Update my employee row to set my Country to UK. |
| DELETE Employees WHERE FirstName = 'Mark' and LastName = 'Price' | Delete my employees row. |
| DELETE Employees | Delete all rows in the *Employees* table and record those deletions in the transaction log. |
| TRUNCATE TABLE Employees | DELETE all rows in the *Employees* table more efficiently because it does not log the individual row deletions. |

## Example DDL Statements
| Example | Description |
|---------|-------------|
| CREATE TABLE dbo.Shippers ( ShipperId INT PRIMARY KEY CLUSTERED, CompanyName NVARCHAR(40) ); | Create a table to store shippers. |
|ALTER TABLE Shippers ADD Country NVARCHAR(40) | Add a column to a table. |
| CREATE NONCLUSTERED INDEX IX_Country ON Shippers(Country) | Add a non-clustered index for a column in a table. |
| CREATE INDEX IX_FullName ON Employees(LastName, FirstName DESC) WITH (DROP_EXISTING = ON) | Change an aggregate index with multiple columns and control the sort oder. |
| DROP TABLE Employees | Delete the Employees table. If it does not exist, then this throws an error. |
| DROP TABLE IF EXISTS Employees | Delete the Employees table if it already exists. This avoids the potential error from using the statement in the previous row. |
| IF OBJECT_ID(N'Employees', N'U') IS NOT NULL | Check if a table exists. The **N** prefix before a text literal means Unicode. 'U' means a user table as opposed to a system table. |

## Important Types in ADO.NET SqlClient
| Type | Properties | Methods | Description |
|------|------------|---------|-------------|
| SqlConnection | ConnectionString, State, ServerVersion | Open, Close, CreateCommand, RetrieveStatistics | Manage the connection to the database. |
| SqlConnectionStringBuilder | InitialCatalog, DataSource, Encrypt, UserID, Password, ConnectTimeout, and so on. | Clear, ContainsKey, Remove | Build a valid connection string for a SQL Server database, After setting all the relevant individual properties, get the ConnectionString property. |
| SqlCommand | Connection, CommandType, CommandText, Parameters, Transaction | ExecuteReader, ExecuteNonQuery, ExecuteXmlReader, CreateParameter | Configure the command to execute. |
| SqlParameter | ParameterName, Value, DbType, SqlValue, SqlDbType, Direction, IsNullable | | Configure a parameter for a command. |
| SqlDataReader | FieldCount, HasRows, IsClosed, RecordsAffected | Read, Close, GetOrdinal, GetInt32, GetString, GetDecimal, GetFieldValue<<T>T> | Process the result set from executing a query. |

## Connection Statistics That Can Be Tracked
| Key | Description |
|-----|-------------|
| BuffersReceived, BuffersSent, BytesReceived, BytesSent | Data is transmitted as bytes stored in buffers. |
| CursorOpens | Cursors are an expensive operation because they require state on the server, and should be avoided when possible. |
| Prepares, PreparedExecs, UnpreparedExecs | Number of prepares (compilations), executions of prepared commands, and executions of unprepared commands. |
| SelectCount, SelectRows | Number of *SELECT* statements and rows returned by *SELECT* statements. |
| ServerRoundTrips, SumResultSets, Transactions | Number of server round trips, result sets, and transactions. |
| ConnectionTime, ExecutionTime, NetworkServerTime | Time in milliseconds spent connected, executing commands, or due to the network. |

## Dapper Query<T<T>> Extension Method Parameters
| Parameter | Description |
|-----------|-------------|
| string sql | This is the only mandatory parameter. It is either the text of a SQL command or the name of a stored procedure. |
| object param = null | A complex object for passing parameters used in the query. This can be an anonymous type. |
| IDbTransaction transaction = null | To manage distributed transactions. |
| bool buffered = true | By default, it will buffer the entire reader on return. With large datasets, you can minimize memory and only load objects as needed by setting buffered to false,=. |
| int? commandTimeout = null | To change the default command timeout. |
| commandType? commandType = null) | To switch to a stored procedure instead of the default of text. |

## Practice Questions
1. Which NuGet package should you reference in a .NET project to get the best performance when working with data in SQL Server?  
**Answer: Microsoft.Data.SqlClient**
2. What is the safest way to define a database connection string?  
**Answer: Use SqlConnectionStringBuilder, set its properties like DataSource, InitialCatalog, UserID, and Password, and then read its ConnectionString property.**
3. What must T-SQL parameters and variables be prefixed with?  
**Answer: @**
4. What must you do before reading an output parameter?  
**Answer: Close the data reader**
5. What type does Dapper add its extension methods to?  
**Answer: Any type that implements IDbConnection**
6. What are the two most commonly used extension methods provided by Dapper?  
**Answer: Query<T<T>> and Execute** 