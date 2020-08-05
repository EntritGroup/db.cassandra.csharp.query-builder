# db.cassandra.csharp.query-builder
# CassandraQueryBuilder
Query builder for the Cassandra database



## What is this repository for? ###

### Quick summary
This library makes it easy for developers to create queries in code for the Cassandra database. The advantages with this is for developers to easily change names in tables, columns etc. during development time using refactor as well as creating a good overview of all names, variables etc used.
### Version 0.1.0
### Licence

## Using the library

### Create column variables
public static readonly Column USER_ID = new Column("column_name", ColumnType.UUID);

#### Column Types
As per the documentation for respective type: [https://cassandra.apache.org/doc/latest/cql/types.html](https://cassandra.apache.org/doc/latest/cql/types.html)

- BOOLEAN
- DOUBLE
- INTEGER
- TEXT
- TIMESTAMP
- TIMEUUID
- UUID
- COUNTER
- INET
- LIST\<Column type as above\> e.g. LIST\<TEXT\> or LIST\<UUID\>


#### Examples
- [Sample project](samples/ExampleProject/DBColumns.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Columns.cs)


### Query Strings
To create query strings use the following code.

Note: Important that the order of the .SetWhereVariables is the same as the order of the columns in the table.

#### Create keyspace query

- [Sample project](samples/ExampleProject/Queries/CreateKeyspace_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_CreateKeyspace.cs)

#### Drop keyspace query
- [Sample project](samples/ExampleProject/Queries/DropKeyspace_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_DropKeyspace.cs)

#### Create table query
- [Sample project](samples/ExampleProject/Queries/CreateTable_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_Tables.cs)

#### Create materalized view query
- [Sample project](samples/ExampleProject/Queries/CreateMaterializedView_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_MaterializedViews.cs)

#### Insert query
- [Sample project](samples/ExampleProject/Queries/Insert_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_Insert.cs)

#### Select query
- [Sample project](samples/ExampleProject/Queries/Select_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_Select.cs)

#### Update query
- [Sample project](samples/ExampleProject/Queries/Update_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_Update.cs)

#### Delete query
- [Sample project](samples/ExampleProject/Queries/Delete_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_Delete.cs)

#### Counter query
- [Sample project](samples/ExampleProject/Queries/Counter_Queries.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Queries/UT_UpdateCounter.cs)


## How do I get set up? ###

### Summary of set up
Nuget
### Configuration
### Dependencies
None
### How to run tests
If you have cloned the whole repository, you will have one folder called tests. In this folder, type "dotnet test" in order to make sure this library works as intended.
### Deployment instructions

## Contribution guidelines ###

### Writing tests
### Code review
### Other guidelines

## Who do I talk to? ###

### Repo owner or admin
### Other community or team contact