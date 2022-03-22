# kvidrer
kvidrer er et forsøg på at lave https://youtu.be/JnEH9tYLxLk med dotnet og svelte.

lav database
```sql
CREATE TABLE data (
    Id serial,
    Timestamp timestamp ,
    Name varchar(255) NOT NULL,
    Content text,
    PRIMARY KEY (id)
);
```
