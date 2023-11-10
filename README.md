# WebShopProject

## General purpose E-commerce Application
This is a general purpose E-commerce app that was developed using .NET Entity Framework code-first approach using SQL DB.
1. It supports features like:
      * User registration
      * Adding products with images
      * Adding product categories
      * Product management (admin)
      * User management (admin)
      * Order management (admin)
      * Cart and order (order simulated, payment not implemented)
      * product API

## Info
Database can be created using existing migrations and having SQL server installed on the machine.
It can be done using powershell or Visual Studio PM console after entering your ServerName and newDatabaseName in connection string => appsettings.json:

 	>PM update-database
```

 "ConnectionStrings": {
    "DefaultConnection": "Server={ServerName};Database={newDatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  },

```
Migrations also create admin account that can be used for Demo.

```
username: admin@admin.com
password: Admin123!
```

API for products information.
```
api/ProductAPI
api/ProductAPI/{productId}
```


