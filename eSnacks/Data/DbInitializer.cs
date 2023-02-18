using eSnacks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace eSnacks.Data;

public static class DbInitializerExtension
{
    public static IApplicationBuilder SeedSqlite(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            DbInitializer.Initialize(context);
            DbInitializer.SeedUsersAndRoles(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while seeding the database.");
        }

        return app;
    }
}

internal class DbInitializer
{
    public static async void Initialize(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        await context.Database.EnsureCreatedAsync();

        if (!context.Categories.Any())
        {
            // Menu Categories
            var categories = new List<Category>
            {
                new()
                {
                    CategoryName = "Nuggets",
                    Description = "Golden and delicious"
                },
                new()
                {
                    CategoryName = "Dinner",
                    Description = "Main courses"
                },
                new()
                {
                    CategoryName = "Desserts",
                    Description = "Sweet treats"
                },
                new()
                {
                    CategoryName = "Launch",
                    Description = "Hearty and satisfying main dishes, perfect for a midday meal."
                },
                new()
                {
                    CategoryName = "Cocktails",
                    Description = "Creative and refreshing mixed drinks, perfect for sipping and socializing."
                },
                new()
                {
                    CategoryName = "Addons",
                    Description =
                        "Additional items that can take your meal to the next level, from savory sauces to crispy toppings."
                },
                new()
                {
                    CategoryName = "Cold drinks",
                    Description =
                        "Refreshing and thirst-quenching beverages, perfect for hot days or any time you need a cool and tasty pick-me-up."
                },
                new()
                {
                    CategoryName = "Soups",
                    Description =
                        "Warm and comforting liquid dishes, perfect for a chilly day or when you need some comfort food."
                },
                new()
                {
                    CategoryName = "Breakfasts",
                    Description =
                        "Wholesome and satisfying dishes to start your day off right, with options for every taste and diet."
                },
                new()
                {
                    CategoryName = "Burgers",
                    Description =
                        "Juicy and flavorful sandwiches that are sure to satisfy your hunger, with a variety of toppings and styles to choose from."
                },
                new()
                {
                    CategoryName = "Ice creams",
                    Description =
                        "Creamy and indulgent frozen desserts, perfect for a sweet treat any time of day or year."
                },
                new()
                {
                    CategoryName = "French fries",
                    Description =
                        "Crispy and golden potato strips that make the perfect side dish for burgers, sandwiches, or anything else on the menu."
                },
                new()
                {
                    CategoryName = "Sushi",
                    Description =
                        "A traditional Japanese food consisting of a steam-cooked rice ball combined with raw fish slices (sashimi) and tuna ."
                },
                new() {CategoryName = "Chicken", Description = "Delicious fat han."}
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }


        if (!context.Cities.Any())
        {
            // Cities
            var cities = new List<City>
            {
                new() {CityName = "Warszawa", ZipCode = "00-001"},
                new() {CityName = "Kraków", ZipCode = "31-001"},
                new() {CityName = "Gdańsk", ZipCode = "80-001"},
                new() {CityName = "Poznań", ZipCode = "61-001"},
                new() {CityName = "Wrocław", ZipCode = "50-001"},
                new() {CityName = "Lódź", ZipCode = "90-001"},
                new() {CityName = "Szczecin", ZipCode = "70-001"},
                new() {CityName = "Katowice", ZipCode = "40-001"},
                new() {CityName = "Bydgoszcz", ZipCode = "85-001"},
                new() {CityName = "Berlin", ZipCode = "10115"},
                new() {CityName = "Paris", ZipCode = "75001"},
                new() {CityName = "Madrid", ZipCode = "28001"},
                new() {CityName = "Rome", ZipCode = "00184"},
                new() {CityName = "Amsterdam", ZipCode = "1012 JS"},
                new() {CityName = "Brussels", ZipCode = "1000"},
                new() {CityName = "Vienna", ZipCode = "1010"},
                new() {CityName = "Dublin", ZipCode = "D02 YX67"},
                new() {CityName = "London", ZipCode = "SW1A 2AA"},
                new() {CityName = "Stockholm", ZipCode = "111 57"},
                new() {CityName = "Oslo", ZipCode = "0187"}
            };

            context.Cities.AddRange(cities);
            await context.SaveChangesAsync();
        }


        if (!context.Restaurants.Any())
        {
            // Restaurants
            var restaurants = new List<Restaurant>
            {
                new()
                {
                    RestaurantName = "Pod Koziołkiem", Address = "ul. Floriańska 33, Kraków",
                    PhotoUrl = new Uri(
                        "https://images.pexels.com/photos/260922/pexels-photo-260922.jpeg?auto=compress&cs=tinysrgb&w=1600"),
                    Description =
                        "A cozy restaurant serving traditional Polish cuisine, with a focus on hearty meat dishes.",
                    CityId = 2
                },
                new()
                {
                    RestaurantName = "Krowarzywa", Address = "ul. Szewska 27, Kraków",
                    PhotoUrl = new Uri(
                        "https://images.pexels.com/photos/1581554/pexels-photo-1581554.jpeg?auto=compress&cs=tinysrgb&w=1600"),
                    Description = "A vegan burger joint with a variety of plant-based options and delicious sides.",
                    CityId = 2
                },
                new()
                {
                    RestaurantName = "Bistro Kaprys", Address = "ul. Nowy Świat 32, Warsaw",
                    PhotoUrl = new Uri(
                        "https://images.pexels.com/photos/941861/pexels-photo-941861.jpeg?auto=compress&cs=tinysrgb&w=1600"),
                    Description = "A charming bistro with a great selection of sandwiches, salads, soups and pastas.",
                    CityId = 1
                },
                new()
                {
                    RestaurantName = "Bar Mleczny Prasowy", Address = "ul. Marszałkowska 10/16, Warsaw",
                    PhotoUrl = new Uri(
                        "https://images.pexels.com/photos/1484516/pexels-photo-1484516.jpeg?auto=compress&cs=tinysrgb&w=1600"),
                    Description = "A classic milk bar with a variety of inexpensive and delicious Polish dishes.",
                    CityId = 1
                },
                new()
                {
                    RestaurantName = "Nino's", Address = "ul. Piotrkowska 120, Łódź",
                    PhotoUrl = null,
                    Description =
                        "A modern Italian restaurant with a great selection of pizzas, pastas, and seafood dishes.",
                    CityId = 6
                },
                new()
                {
                    RestaurantName = "Sakana Sushi", Address = "ul. Mikołajska 4, Kraków",
                    PhotoUrl = null,
                    Description =
                        "A sushi restaurant with a variety of nigiri, maki, and sashimi rolls, as well as other Japanese dishes.",
                    CityId = 2
                },
                new()
                {
                    RestaurantName = "La Ruina", Address = "ul. Wielopole 15, Kraków",
                    PhotoUrl = null,
                    Description =
                        "A cozy restaurant with a variety of Mediterranean and European dishes, and a great selection of wines.",
                    CityId = 2
                },
                new()
                {
                    RestaurantName = "Pierogarnia Stary Młyn", Address = "ul. Sławkowska 26, Kraków",
                    PhotoUrl = null,
                    Description =
                        "A casual restaurant serving a variety of traditional Polish dumplings with different fillings.",
                    CityId = 2
                },
                new()
                {
                    RestaurantName = "Mihiderka", Address = "ul. Włodkowica 16, Wrocław",
                    PhotoUrl = null,
                    Description =
                        "A Japanese restaurant with a great selection of sushi, ramen, and other delicious dishes.",
                    CityId = 5
                },
                new()
                {
                    RestaurantName = "Cukiernia Sowa", Address = "ul. Świdnicka 11, Wrocław",
                    PhotoUrl = null,
                    Description =
                        "A cozy café and patisserie with a great selection of pastries, cakes, and other sweet treats.",
                    CityId = 5
                }
            };

            context.Restaurants.AddRange(restaurants);
            await context.SaveChangesAsync();
        }

        if (!context.MenuItems.Any())
        {
            var menuitems = new List<MenuItem>
            {
                new()
                {
                    ItemName = "Buffalo chicken nuggets",
                    Description = "Spicy and crispy chicken nuggets served with blue cheese dipping sauce",
                    PhotoUrl = null, Ingredients = "Chicken breast, hot sauce, bread crumbs, egg, blue cheese",
                    Available = true, Price = 12.99d, CategoryId = 1, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Classic Cheeseburger",
                    Description = "Our signature burger with a juicy beef patty and melted cheese", PhotoUrl = null,
                    Ingredients = "Beef patty, cheese, lettuce, tomato, onion, pickles, ketchup, mayo, brioche bun",
                    Available = true, Price = 9.99d, CategoryId = 10, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Mushroom Swiss Burger",
                    Description = "A flavorful burger with sautéed mushrooms and Swiss cheese", PhotoUrl = null,
                    Ingredients =
                        "Beef patty, Swiss cheese, sautéed mushrooms, lettuce, tomato, onion, pickles, ketchup, mayo, brioche bun",
                    Available = true, Price = 11.99d, CategoryId = 10, RestaurantId = 2
                },
                new()
                {
                    ItemName = "Spicy Jalapeño Burger",
                    Description = "A fiery burger with jalapeños, pepper jack cheese, and our special sauce",
                    PhotoUrl = null,
                    Ingredients =
                        "Beef patty, pepper jack cheese, jalapeños, lettuce, tomato, onion, pickles, special sauce, brioche bun",
                    Available = true, Price = 12.99d, CategoryId = 10, RestaurantId = 2
                },
                new()
                {
                    ItemName = "Green Curry Chicken",
                    Description = "Savory green curry with chicken, coconut milk, and vegetables", PhotoUrl = null,
                    Ingredients =
                        "Chicken, green curry paste, coconut milk, green beans, bell peppers, bamboo shoots, Thai basil, rice",
                    Available = true, Price = 14.99d, CategoryId = 14, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Red Curry Shrimp",
                    Description = "Spicy red curry with shrimp, coconut milk, and vegetables", PhotoUrl = null,
                    Ingredients =
                        "Shrimp, red curry paste, coconut milk, bell peppers, bamboo shoots, Thai basil, rice",
                    Available = true, Price = 16.99d, CategoryId = 9, RestaurantId = 7
                },
                new()
                {
                    ItemName = "Spaghetti Bolognese", Description = "Classic spaghetti with a rich meaty sauce",
                    PhotoUrl = null,
                    Ingredients = "Spaghetti, ground beef, tomato sauce, onion, garlic, herbs, Parmesan cheese",
                    Available = true, Price = 10.99d, CategoryId = 2, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Fettuccine Alfredo",
                    Description = "Creamy fettuccine pasta with Parmesan cheese and garlic", 
                    PhotoUrl = null,
                    Ingredients = "Fettuccine pasta, heavy cream, Parmesan cheese, garlic, herbs", 
                    Available = true,
                    Price = 12.99d, CategoryId = 2, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Penne Arrabbiata",
                    Description = "Spicy penne pasta with tomato sauce and chili peppers", 
                    PhotoUrl = null,
                    Ingredients = "Penne pasta, tomato sauce, garlic, chili peppers, Parmesan cheese", 
                    Available = true,
                    Price = 11.99d, CategoryId = 2, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Pierogi ruskie",
                    Description = "Traditional Polish dumplings filled with potatoes, cheese, and onions",
                    PhotoUrl = null, Ingredients = "Potatoes, cheese, onions, flour, eggs", 
                    Available = true,
                    Price = 20.99d, CategoryId = 2, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Stew", Description = "A hearty stew made with sauerkraut, various meats, and spices",
                    PhotoUrl = null,
                    Ingredients = "Sauerkraut, beef, pork, sausage, mushrooms, onions, carrots, spices",
                    Available = true, Price = 25.99d, CategoryId = 2, RestaurantId = 1
                },
                new()
                {
                    ItemName = "The Classic Burger",
                    Description = "A juicy plant-based burger patty, lettuce, tomato, onion, pickles, and special sauce",
                    PhotoUrl = null,
                    Ingredients = "Plant-based burger patty, lettuce, tomato, onion, pickles, sauce, bun",
                    Available = true, Price = 15.99d, CategoryId = 10, RestaurantId = 2
                },
                new()
                {
                    ItemName = "Vegan Poutine",
                    Description = "Crispy french fries, vegan gravy, and melty cheese curds", PhotoUrl = null,
                    Ingredients = "Potatoes, vegan gravy, vegan cheese, herbs", Available = true, Price = 12.99d,
                    CategoryId = 12, RestaurantId = 2
                },
                new()
                {
                    ItemName = "Caprese Salad",
                    Description = "Fresh tomatoes, mozzarella, and basil, drizzled with balsamic glaze",
                    PhotoUrl = null, Ingredients = "Tomatoes, mozzarella, basil, balsamic glaze, olive oil",
                    Available = true, Price = 18.99d, CategoryId = 4, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Grilled Cheese Sandwich",
                    Description = "Toasted bread with melted cheese, served with a side of homemade tomato soup",
                    PhotoUrl = null, Ingredients = "Bread, cheese, tomato soup", Available = true, Price = 14.99d,
                    CategoryId = 9, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Zapiekanka",
                    Description =
                        "A classic Polish street food, featuring a baguette with mushrooms, cheese, and ketchup",
                    PhotoUrl = null, Ingredients = "Baguette, mushrooms, cheese, ketchup", Available = true,
                    Price = 8.99d, CategoryId = 4, RestaurantId = 4
                },
                new()
                {
                    ItemName = "Rosół z kury",
                    Description = "A delicious, traditional chicken soup with carrots, parsley, and noodles",
                    PhotoUrl = null, Ingredients = "Chicken, carrots, parsley, noodles, water", Available = true,
                    Price = 10.99d, CategoryId = 8, RestaurantId = 4
                },
                new()
                {
                    ItemName = "Bigos po łemkowsku",
                    Description = "A spicy version of the traditional Polish stew, with extra peppers and sausage",
                    PhotoUrl = null, Ingredients = "Sauerkraut, beef, pork, sausage, peppers, onions, spices",
                    Available = true, Price = 22.99d, CategoryId = 2, RestaurantId = 5
                },
                new()
                {
                    ItemName = "Bigos", Description = "Traditional Polish stew made with meat and sauerkraut",
                    PhotoUrl = null, Ingredients = "Pork, beef, sauerkraut, onion, mushrooms, spices", Available = true,
                    Price = 28.50d, CategoryId = 2, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Placki ziemniaczane",
                    Description = "Crispy potato pancakes served with sour cream and apple sauce", PhotoUrl = null,
                    Ingredients = "Potatoes, onion, egg, flour, salt, pepper, oil", Available = true, Price = 16.50d,
                    CategoryId = 2, RestaurantId = 1
                },
                new()
                {
                    ItemName = "Kotlet schabowy",
                    Description = "Classic Polish pork cutlet served with potatoes and sauerkraut", PhotoUrl = null,
                    Ingredients = "Pork loin, breadcrumbs, egg, potatoes, sauerkraut, butter, flour", Available = true,
                    Price = 32.50d, CategoryId = 2, RestaurantId = 3
                },
                new()
                {
                    ItemName = "Pierogi z mięsem",
                    Description = "Handmade dumplings filled with meat and served with fried onions and sour cream",
                    PhotoUrl = null, Ingredients = "Flour, egg, water, ground beef, onion, spices, butter, sour cream",
                    Available = true, Price = 24.50d, CategoryId = 2, RestaurantId = 2
                },
                new()
                {
                    ItemName = "Żurek",
                    Description = "Traditional Polish sour soup made with fermented rye flour, sausage, and boiled egg",
                    PhotoUrl = null,
                    Ingredients = "Fermented rye flour, sausage, boiled egg, potato, onion, garlic, marjoram",
                    Available = true, Price = 14.50d, CategoryId = 8, RestaurantId = 1
                },
                new()
                {
                    ItemName = "BBQ Chicken Wings",
                    Description = "Juicy and tender chicken wings smothered in BBQ sauce.",
                    PhotoUrl = null,
                    Ingredients = "Chicken wings, BBQ sauce, salt, pepper, garlic powder.",
                    Available = true,
                    Price = 10.99d,
                    CategoryId = 1,
                    RestaurantId = 1
                },
                new()
                {
                    ItemName = "Cheeseburger",
                    Description =
                        "Freshly grilled beef patty topped with melted cheese, lettuce, tomato, onion, and pickles.",
                    PhotoUrl = null,
                    Ingredients =
                        "Beef patty, American cheese, lettuce, tomato, onion, pickles, ketchup, mustard, mayonnaise, salt, pepper.",
                    Available = true,
                    Price = 8.99d,
                    CategoryId = 10,
                    RestaurantId = 1
                },
                new()
                {
                    ItemName = "Chocolate Cake",
                    Description = "Moist and rich chocolate cake with chocolate frosting and a cherry on top.",
                    PhotoUrl = null,
                    Ingredients =
                        "Flour, sugar, cocoa powder, baking powder, baking soda, eggs, milk, vegetable oil, vanilla extract, salt, butter, powdered sugar, cocoa powder, milk, vanilla extract, cherry.",
                    Available = true,
                    Price = 5.99d,
                    CategoryId = 3,
                    RestaurantId = 1
                },
                new()
                {
                    ItemName = "Classic Burger",
                    Description =
                        "Our classic burger comes with juicy beef patty, lettuce, tomato, pickles, and cheddar cheese",
                    PhotoUrl = null,
                    Ingredients = "Beef patty, lettuce, tomato, pickles, cheddar cheese",
                    Available = true,
                    Price = 12.50d,
                    CategoryId = 10,
                    RestaurantId = 1
                },
                new()
                {
                    ItemName = "Fried Chicken Wings",
                    Description =
                        "Crispy fried chicken wings with your choice of sauce: Buffalo, BBQ, or Honey Mustard",
                    PhotoUrl = null,
                    Ingredients = "Chicken wings, flour, seasoning, sauce",
                    Available = true,
                    Price = 9.99d,
                    CategoryId = 1,
                    RestaurantId = 1
                },
                new()
                {
                    ItemName = "Brownie Sundae",
                    Description = "A warm brownie with vanilla ice cream, whipped cream, and chocolate syrup",
                    PhotoUrl = null,
                    Ingredients = "Brownie, vanilla ice cream, whipped cream, chocolate syrup",
                    Available = true,
                    Price = 7.99d,
                    CategoryId = 3,
                    RestaurantId = 1
                }
            };

            context.MenuItems.AddRange(menuitems);
            await context.SaveChangesAsync();
        }
    }

    public static async void SeedUsersAndRoles(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        await context.Database.EnsureCreatedAsync();

        //Roles

        var roles = new[] {"Administrator", "Moderator", "User"};

        var roleStore = new RoleStore<IdentityRole>(context);

        foreach (var role in roles)
            if (!context.Roles.Any(r => r.Name == role))
                await roleStore.CreateAsync(new IdentityRole {Name = role, NormalizedName = role.ToUpper()});

        await context.SaveChangesAsync();


        //Users
        var userStore = new UserStore<eSnacksUser>(context);

        var adminUserEmail = "admin@eSnacks.com";
        var adminUserName = adminUserEmail;

        if (!context.Users.Any(u => u.Email.Equals(adminUserEmail)))
        {
            var newAdminUser = new eSnacksUser
            {
                FirstName = "Little",
                LastName = "Bro",
                Address = "foobar",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "+48111222333",
                PhoneNumberConfirmed = true,
                UserName = adminUserName,
                NormalizedUserName = adminUserName.ToUpper(),
                Email = adminUserEmail,
                NormalizedEmail = adminUserEmail.ToUpper(),
                EmailConfirmed = true
            };

            var password = new PasswordHasher<eSnacksUser>();
            var hashed = password.HashPassword(newAdminUser, "zaq1@WSX");
            newAdminUser.PasswordHash = hashed;

            await userStore.CreateAsync(newAdminUser);
            await userStore.AddToRoleAsync(newAdminUser, "Administrator".ToUpper());
        }

        var appUserEmail = "user@eSnacks.com";
        var appUserName = appUserEmail;

        if (!context.Users.Any(u => u.Email.Equals(appUserEmail)))
        {
            var newAppUser = new eSnacksUser
            {
                FirstName = "Bob",
                LastName = "Builder",
                Address = "Warsaw ul. Kopernika 23/6B",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "+48333222111",
                PhoneNumberConfirmed = true,
                UserName = appUserName,
                NormalizedUserName = appUserName.ToUpper(),
                Email = appUserEmail,
                NormalizedEmail = appUserEmail.ToUpper(),
                EmailConfirmed = true
            };

            var password = new PasswordHasher<eSnacksUser>();
            var hashed = password.HashPassword(newAppUser, "zaq1@WSX");
            newAppUser.PasswordHash = hashed;

            await userStore.CreateAsync(newAppUser);
            await userStore.AddToRoleAsync(newAppUser, "User".ToUpper());
        }

        await context.SaveChangesAsync();
    }
}