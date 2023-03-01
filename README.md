# eSnacks
A web application that allows you to order food from various restaurants. The application is written in ASP.NET MVC .NET 6 using Entity Framework and SqlServer.

### Requirements
The application requires the following tools to be installed:
```
- .NET 6
- Visual Studio, Visual Studio Code or other IDEs
- Microsoft SQL Server 2012 or newer
```
## Install and run
Clone the repository.
```
https://github.com/jaugustyn/eSnacksMVC.git
```
Open the project in IDE.
Open the package manager console and run the following commands to restore packages and create the database.
```
Dotnet restore
Dotnet build
Add-Migration Init
Update-Database
```

Run the application.

### Seeded data
The application has seeded data, including two users. Below are the email addresses and passwords of these users:
```
User
Email: user@eSnacks.com
Password: zaq1@WSX

Admin
Email: admin@eSnacks.com
Password: zaq1@WSX
```
These credentials can be used to log into the application and test its functionality.
However, for security purposes, it is recommended to change the password of these users after logging into the application.

## Description
### Models
- `eSnacksUser:` user model, extends the IdentityUser model with fields for first name, last name, address and date of birth.
- `Category:` category model, contains category name, description.
- `City:` city model, includes city name, postal code.
- `Restaurant:` restaurant model, contains name, address, photo, description, relation with cities.
- `MenuItem:` menu item model, contains name, description, photo, ingredients, unit price, relation with categories and restaurant.
- `PlacedOrder:` model of a placed order, contains the date the order was placed, the estimated time for delivery, the delivery address.
- `InOrder:` Items in order model, contains quantity, price, comment, related to the placedOrder and menu item.
- `OrderStatus:` model represents the status of an order, contains a Status field that specifies the status name. It is related to PlacedOrders.

### Functionality description
- User registration and login
- Selecting a city and displaying a list of restaurants in that city
- Restaurant selection and menu display
- Adding menu items to the shopping cart
- Viewing and modifying the shopping cart
- Placing an order
- Viewing order history
- Managing user profile
