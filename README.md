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
```
Column USER_ID = new Column("user_id", ColumnType.TEXT);

Column NAME = new Column("name", ColumnType.TEXT);

Column NAME_STATIC = new Column("name_s", ColumnType.TEXT, true); //true in the end indicates a static column
```        

### Query Strings
To create query strings use the following code.

Note: Important that the order of the .SetWhereVariables is the same as the order of the columns in the table.

#### Create keyspace query
---
```
String NAME_OF_QUERY_STRING = new Insert()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetColumns(
        USER_ID,
        NAME
    )
    .ToString())
    ;
```

#### Drop keyspace query
```
String NAME_OF_QUERY_STRING = new DropKeyspace()
    .SetName(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .ToString()
;
```

#### Create table query
```
 String NAME_OF_QUERY_STRING = new CreateTable()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetPartitionKeys(
        USER_ID
    )
    .SetClusteringKeys(
        ...
    )
    .SetColumns(
        NAME, //The name of the user
    )
    .SetCompactionStrategy(CompactionStrategy.LeveledCompactionStrategy)
    .ToString()
;

```

#### Create materalized view query
```
String NAME_OF_QUERY_STRING = CreateMaterializedView()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetToTableName(STRING_VARIABLE_WITH_MATERIALIZED_VIEW_TABLE_NAME)
    .SetFromTableName(STRING_VARIABLE_WITH_THE_ORIGINAL_TABLE_NAME)
    .SetPartitionKeys(NAME)
    .SetClusteringKeys(USER_ID)
    .SetCompactionStrategy(CompactionStrategy.LeveledCompactionStrategy),
    .SetVariables(...)
    .ToString()
;
```

#### Insert query
```
String NAME_OF_QUERY_STRING = new Insert()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetColumns(
        USER_ID,
        NAME
    )
    .ToString())
;
```

#### Get/Select query
No limit on responses
```
String NAME_OF_QUERY_STRING = new Select()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetColumns(
        USER_ID,
        NAME
    )
    .SetWhereColumns(
        USER_ID
    )
    .SetLimit() //Here you can specify the limit as a varaible in the query
    .ToString())
;
```

Changeable limit on responses
```
String NAME_OF_QUERY_STRING = new Select()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetColumns(
        USER_ID,
        NAME
    )
    .SetWhereColumns(
        USER_ID
    )
    .SetLimit() //Here you need to specify the limit as a varaible in the query during runtime.
    .ToString())
;
```


Fixed limit on responses
```
String NAME_OF_QUERY_STRING = new Select()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetColumns(
        USER_ID,
        NAME
    )
    .SetWhereColumns(
        USER_ID
    )
    .SetLimit(10) //Will return up to 10 rows in the response
    .ToString())
;
```


#### Update query
```
String NAME_OF_QUERY_STRING = new Update()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetVariables(NAME) //Column variable of what to be updated 
    .SetWhereVariables(
        USER_ID,
        ...
    )
    .ToString())
;

```

#### Delete query
```
String NAME_OF_QUERY_STRING = new Delete()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetWhereVariables(
        USER_ID,
        ...
    )
    .ToString())
;
```

#### Counter query
```
String NAME_OF_QUERY_STRING = new UpdateCounter()
    .SetKeyspace(STRING_VARIABLE_WITH_KEYSPACE_NAME)
    .SetTableName(STRING_VARIABLE_WITH_TABLE_NAME)
    .SetVariables(NAME) //Column variable of what to be updated 
    .SetIncreaseBy(1) //You can also set -1 here to decrease
    .SetWhereVariables(
        USER_ID,
    )
    .ToString())
;
```


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