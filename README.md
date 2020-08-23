# C# Query Builder for Apache Cassandra
Query builder for the Cassandra database

## Description

This library makes it easy for developers to create CQL queries in code to be used as prepared statements for the Cassandra database. Perfect to use together with the Datastax CassandraCSharpDriver for prepared statements and batch statements.

## Installing the library
Get it on Nuget

```
PM> Install-Package CassandraCSharpQueryBuilder
```

## Using the library

There are many examples in the sample project as well as in the test project. Most common use cases below are linked to examples in these projects.

### Create columns
For creating tables as well as creating queries, you need to specify columns and their respective type. 

#### Examples of creating columns
- [Sample project](samples/ExampleProject/DBColumns.cs)
- [Test project](tests/CassandraQueryBuilder.Tests.UT/Columns.cs)

#### Column Types
As per the documentation for respective type: [https://cassandra.apache.org/doc/latest/cql/types.html](https://cassandra.apache.org/doc/latest/cql/types.html)

### Query Strings

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

## Contribution guidelines ###
There are many ways in which you can participate in the project, for example:

- Submit bugs and feature requests, and help us verify as they are checked in
- Review source code changes
- Review the documentation and make pull requests for anything from typos to new content

### Tests
You can run the tests by typing "dotnet test .\tests\CassandraQueryBuilder.Tests.UT\"

## Releases

### Version 1.0.0
Initial release