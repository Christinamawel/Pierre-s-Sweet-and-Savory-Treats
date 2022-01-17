# _Pierre's Sweet and Savory Treats_        

#### By **Christina Welch**

#### _A program that tracks treats and flavors_

## Technologies Used

* C#
* .net
* ASP.NET Core
* Razor
* CSS
* HTML
* Entity Framework
* SQL

## Description

_End of week friday project for epicodus school made to demonstrate skills in integrating authentication with Identity. it tracks a many to many relationship between treats and flavors but requires a log in to add to the database._

## Setup/Installation Requirements

* Clone this repository to your Desktop.
* Open Pierre-s-Sweet-and-Savory-Treats/Pierres in VScode or a code editor of your choice.
* create a new file and name it appsettings.json
* in this file add the following:

```
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=3306;database=Christina_Welch;uid=root;pwd=[YOUR-PASSWORD-HERE];"
    }
}
```

Note: make sure to replace [YOUR-PASSWORD-HERE] with your password for SQL.
* type dotnet ef database update into the terminal then type dotnet run

## Known Bugs

## License

MIT License

Copyright (c) [2022] [Christina Welch]