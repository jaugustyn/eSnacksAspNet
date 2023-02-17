using eSnacks.Models;
using Microsoft.EntityFrameworkCore.Internal;

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
    public static void Initialize(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        context.Database.EnsureCreated();

        if (!context.Categories.Any())
        {
            // Menu Categories
            var categories = new List<Category>
            {
                new Category { CategoryName = "Nuggets", Description = "Golden and delicious" },
                new Category { CategoryName = "Dinner", Description = "Main courses" },
                new Category { CategoryName = "Desserts", Description = "Sweet treats" },
                new Category { CategoryName = "Launch", Description = "Hearty and satisfying main dishes, perfect for a midday meal." },
                new Category { CategoryName = "Cocktails", Description = "Creative and refreshing mixed drinks, perfect for sipping and socializing." },
                new Category { CategoryName = "Addons", Description = "Additional items that can take your meal to the next level, from savory sauces to crispy toppings." },
                new Category { CategoryName = "Cold drinks", Description = "Refreshing and thirst-quenching beverages, perfect for hot days or any time you need a cool and tasty pick-me-up." },
                new Category { CategoryName = "Soups", Description = "Warm and comforting liquid dishes, perfect for a chilly day or when you need some comfort food." },
                new Category { CategoryName = "Breakfasts", Description = "Wholesome and satisfying dishes to start your day off right, with options for every taste and diet." },
                new Category { CategoryName = "Burgers", Description = "Juicy and flavorful sandwiches that are sure to satisfy your hunger, with a variety of toppings and styles to choose from." },
                new Category { CategoryName = "Ice creams", Description = "Creamy and indulgent frozen desserts, perfect for a sweet treat any time of day or year." },
                new Category { CategoryName = "French fries", Description = "Crispy and golden potato strips that make the perfect side dish for burgers, sandwiches, or anything else on the menu." },
                new Category { CategoryName = "Sushi", Description = "A traditional Japanese food consisting of a steam-cooked rice ball combined with raw fish slices (sashimi) and tuna ." },
                new Category { CategoryName = "Chicken", Description = "Delicious fat han." },
            };
            
            context.Categories.AddRange(categories);
            context.SaveChanges();
        };
        

        
        
        if (!context.Cities.Any())
        {
            // Cities
            var cities = new List<City>
            {
                new City { CityName = "Warszawa", ZipCode = "00-001" },
                new City { CityName = "Kraków", ZipCode = "31-001" },
                new City { CityName = "Gdańsk", ZipCode = "80-001" },
                new City { CityName = "Poznań", ZipCode = "61-001" },
                new City { CityName = "Wrocław", ZipCode = "50-001" },
                new City { CityName = "Lódź", ZipCode = "90-001" },
                new City { CityName = "Szczecin", ZipCode = "70-001" },
                new City { CityName = "Katowice", ZipCode = "40-001" },
                new City { CityName = "Bydgoszcz", ZipCode = "85-001" },
                new City { CityName = "Berlin", ZipCode = "10115" },
                new City { CityName = "Paris", ZipCode = "75001" },
                new City { CityName = "Madrid", ZipCode = "28001" },
                new City { CityName = "Rome", ZipCode = "00184" },
                new City { CityName = "Amsterdam", ZipCode = "1012 JS" },
                new City { CityName = "Brussels", ZipCode = "1000" },
                new City { CityName = "Vienna", ZipCode = "1010" },
                new City { CityName = "Dublin", ZipCode = "D02 YX67" },
                new City { CityName = "London", ZipCode = "SW1A 2AA" },
                new City { CityName = "Stockholm", ZipCode = "111 57" },
                new City { CityName = "Oslo", ZipCode = "0187" },
                new City { CityName = "Copenhagen", ZipCode = "1050" },
                new City { CityName = "Helsinki", ZipCode = "00100" },
                new City { CityName = "Reykjavik", ZipCode = "101" },
                new City { CityName = "Athens", ZipCode = "105 51" },
            };
            
            context.Cities.AddRange(cities);
            context.SaveChanges();
        };


        if (!context.Restaurants.Any())
        {
            // Restaurants
            var restaurants = new List<Restaurant>
            {
                new Restaurant { RestaurantName = "Pod Koziołkiem", Address = "ul. Floriańska 33, Kraków", Description = "A cozy restaurant serving traditional Polish cuisine, with a focus on hearty meat dishes.", CityId = 2},
                new Restaurant { RestaurantName = "Krowarzywa", Address = "ul. Szewska 27, Kraków", Description = "A vegan burger joint with a variety of plant-based options and delicious sides.", CityId = 2 },
                new Restaurant { RestaurantName = "Bistro Kaprys", Address = "ul. Nowy Świat 32, Warsaw", Description = "A charming bistro with a great selection of sandwiches, salads, soups and pastas.", CityId = 1 },
                new Restaurant { RestaurantName = "Bar Mleczny Prasowy", Address = "ul. Marszałkowska 10/16, Warsaw", Description = "A classic milk bar with a variety of inexpensive and delicious Polish dishes.", CityId = 1 },
                new Restaurant { RestaurantName = "Nino's", Address = "ul. Piotrkowska 120, Łódź", Description = "A modern Italian restaurant with a great selection of pizzas, pastas, and seafood dishes.", CityId = 6 },
                new Restaurant { RestaurantName = "Sakana Sushi", Address = "ul. Mikołajska 4, Kraków", Description = "A sushi restaurant with a variety of nigiri, maki, and sashimi rolls, as well as other Japanese dishes.", CityId = 2 },
                new Restaurant { RestaurantName = "La Ruina", Address = "ul. Wielopole 15, Kraków", Description = "A cozy restaurant with a variety of Mediterranean and European dishes, and a great selection of wines.", CityId = 2 },
                new Restaurant { RestaurantName = "Pierogarnia Stary Młyn", Address = "ul. Sławkowska 26, Kraków", Description = "A casual restaurant serving a variety of traditional Polish dumplings with different fillings.", CityId = 2 },
                new Restaurant { RestaurantName = "Mihiderka", Address = "ul. Włodkowica 16, Wrocław", Description = "A Japanese restaurant with a great selection of sushi, ramen, and other delicious dishes.", CityId = 5 },
                new Restaurant { RestaurantName = "Cukiernia Sowa", Address = "ul. Świdnicka 11, Wrocław", Description = "A cozy café and patisserie with a great selection of pastries, cakes, and other sweet treats.", CityId = 5 },
                new Restaurant { RestaurantName = "Curry 61", Address = "Oranienburger Str. 31, 10117 Berlin, Germany", Description = "A popular fast food spot serving delicious currywurst and fries with a variety of sauces.", CityId = 10 },
                new Restaurant { RestaurantName = "Le Comptoir du Relais", Address = "9 Carrefour de l'Odéon, 75006 Paris, France", Description = "A cozy bistro with a chic decor, offering a range of French classics, wine and cheese.", CityId = 11 },
                new Restaurant { RestaurantName = "El Club Allard", Address = "Calle de Ferraz, 2, 28008 Madrid, Spain", Description = "A modern restaurant with creative and elegant dishes, made with seasonal and local ingredients.", CityId = 12 },
                new Restaurant { RestaurantName = "Sora Lella", Address = "Via di Ponte Quattro Capi, 16, 00186 Roma RM, Italy", Description = "A family-run restaurant serving authentic Roman cuisine, with traditional dishes like cacio e pepe and amatriciana.", CityId = 13 },
                new Restaurant { RestaurantName = "De Kas", Address = "Kamerlingh Onneslaan 3, 1097 DE Amsterdam, Netherlands", Description = "A unique farm-to-table restaurant set in a greenhouse, with an ever-changing menu based on seasonal ingredients.", CityId = 14 },
                new Restaurant { RestaurantName = "Plachutta", Address = "Wollzeile 38, 1010 Wien, Austria", Description = "A classic Viennese restaurant known for its famous tafelspitz (boiled beef) and other traditional dishes.", CityId = 16 },
                new Restaurant { RestaurantName = "Chapter One", Address = "18-19 Parnell Square N, Rotunda, Dublin, D01 T3V8, Ireland", Description = "A stylish restaurant with a modern Irish cuisine, using fresh and seasonal ingredients, and a great selection of wines.", CityId = 17 },
                new Restaurant { RestaurantName = "Dinner by Heston Blumenthal", Address = "Mandarin Oriental Hyde Park, 66 Knightsbridge, London SW1X 7LA, United Kingdom", Description = "A top-rated restaurant with a menu inspired by historical British gastronomy, using contemporary techniques and flavors.", CityId = 18 },
                new Restaurant { RestaurantName = "Oaxen Krog", Address = "Beckholmsbron 26, 115 21 Stockholm, Sweden", Description = "A gourmet restaurant with a focus on Nordic cuisine, using local and organic ingredients, and an excellent wine list.", CityId = 19 },
            };
                
            context.Restaurants.AddRange(restaurants);
            context.SaveChanges();
        }

        if (!context.MenuItems.Any())
        {
            var menuitems = new List<MenuItem>
            {
                new MenuItem { ItemName = "Buffalo chicken nuggets", Description = "Spicy and crispy chicken nuggets served with blue cheese dipping sauce", PhotoUrl = null, Ingredients = "Chicken breast, hot sauce, bread crumbs, egg, blue cheese", Available = true, Price = 12.99m, CategoryId = 1, RestaurantId = 1},
                new MenuItem { ItemName = "Classic Cheeseburger", Description = "Our signature burger with a juicy beef patty and melted cheese", PhotoUrl = null, Ingredients = "Beef patty, cheese, lettuce, tomato, onion, pickles, ketchup, mayo, brioche bun", Available = true, Price = 9.99m, CategoryId = 10, RestaurantId = 1 },
                new MenuItem { ItemName = "Mushroom Swiss Burger", Description = "A flavorful burger with sautéed mushrooms and Swiss cheese", PhotoUrl = null, Ingredients = "Beef patty, Swiss cheese, sautéed mushrooms, lettuce, tomato, onion, pickles, ketchup, mayo, brioche bun", Available = true, Price = 11.99m, CategoryId = 10, RestaurantId = 2 },
                new MenuItem { ItemName = "Spicy Jalapeño Burger", Description = "A fiery burger with jalapeños, pepper jack cheese, and our special sauce", PhotoUrl = null, Ingredients = "Beef patty, pepper jack cheese, jalapeños, lettuce, tomato, onion, pickles, special sauce, brioche bun", Available = true, Price = 12.99m, CategoryId = 10, RestaurantId = 2 },
                new MenuItem { ItemName = "Green Curry Chicken", Description = "Savory green curry with chicken, coconut milk, and vegetables", PhotoUrl = null, Ingredients = "Chicken, green curry paste, coconut milk, green beans, bell peppers, bamboo shoots, Thai basil, rice", Available = true, Price = 14.99m, CategoryId = 14, RestaurantId = 1 },
                new MenuItem { ItemName = "Red Curry Shrimp", Description = "Spicy red curry with shrimp, coconut milk, and vegetables", PhotoUrl = null, Ingredients = "Shrimp, red curry paste, coconut milk, bell peppers, bamboo shoots, Thai basil, rice", Available = true, Price = 16.99m, CategoryId = 9, RestaurantId = 7 },
                new MenuItem { ItemName = "Spaghetti Bolognese", Description = "Classic spaghetti with a rich meaty sauce", PhotoUrl = null, Ingredients = "Spaghetti, ground beef, tomato sauce, onion, garlic, herbs, Parmesan cheese", Available = true, Price = 10.99m, CategoryId = 2, RestaurantId = 3 },
                new MenuItem { ItemName = "Fettuccine Alfredo", Description = "Creamy fettuccine pasta with Parmesan cheese and garlic", PhotoUrl = null, Ingredients = "Fettuccine pasta, heavy cream, Parmesan cheese, garlic, herbs", Available = true, Price = 12.99m, CategoryId = 2, RestaurantId = 3 },
                new MenuItem { ItemName = "Penne Arrabbiata", Description = "Spicy penne pasta with tomato sauce and chili peppers", PhotoUrl = null, Ingredients = "Penne pasta, tomato sauce, garlic, chili peppers, Parmesan cheese", Available = true, Price = 11.99m, CategoryId = 2, RestaurantId = 3 },
                new MenuItem { ItemName = "Pierogi ruskie", Description = "Traditional Polish dumplings filled with potatoes, cheese, and onions", PhotoUrl = null, Ingredients = "Potatoes, cheese, onions, flour, eggs", Available = true, Price = 20.99m, CategoryId = 2, RestaurantId = 1 },
                new MenuItem { ItemName = "Bigos", Description = "A hearty stew made with sauerkraut, various meats, and spices", PhotoUrl = null, Ingredients = "Sauerkraut, beef, pork, sausage, mushrooms, onions, carrots, spices", Available = true, Price = 25.99m, CategoryId = 3, RestaurantId = 1 },
                new MenuItem { ItemName = "The Classic Burger", Description = "A juicy plant-based burger patty, lettuce, tomato, onion, pickles, and special sauce", PhotoUrl = null, Ingredients = "Plant-based burger patty, lettuce, tomato, onion, pickles, sauce, bun", Available = true, Price = 15.99m, CategoryId = 10, RestaurantId = 2 },
                new MenuItem { ItemName = "Vegan Poutine", Description = "Crispy french fries, vegan gravy, and melty cheese curds", PhotoUrl = null, Ingredients = "Potatoes, vegan gravy, vegan cheese, herbs", Available = true, Price = 12.99m, CategoryId = 12, RestaurantId = 2 },
                new MenuItem { ItemName = "Caprese Salad", Description = "Fresh tomatoes, mozzarella, and basil, drizzled with balsamic glaze", PhotoUrl = null, Ingredients = "Tomatoes, mozzarella, basil, balsamic glaze, olive oil", Available = true, Price = 18.99m, CategoryId = 4, RestaurantId = 3 },
                new MenuItem { ItemName = "Grilled Cheese Sandwich", Description = "Toasted bread with melted cheese, served with a side of homemade tomato soup", PhotoUrl = null, Ingredients = "Bread, cheese, tomato soup", Available = true, Price = 14.99m, CategoryId = 9, RestaurantId = 3 },
                new MenuItem { ItemName = "Zapiekanka", Description = "A classic Polish street food, featuring a baguette with mushrooms, cheese, and ketchup", PhotoUrl = null, Ingredients = "Baguette, mushrooms, cheese, ketchup", Available = true, Price = 8.99m, CategoryId = 4, RestaurantId = 4 },
                new MenuItem { ItemName = "Rosół z kury", Description = "A delicious, traditional chicken soup with carrots, parsley, and noodles", PhotoUrl = null, Ingredients = "Chicken, carrots, parsley, noodles, water", Available = true, Price = 10.99m, CategoryId = 8, RestaurantId = 4 },
                new MenuItem { ItemName = "Bigos po łemkowsku", Description = "A spicy version of the traditional Polish stew, with extra peppers and sausage", PhotoUrl = null, Ingredients = "Sauerkraut, beef, pork, sausage, peppers, onions, spices", Available = true, Price = 22.99m, CategoryId = 2, RestaurantId = 5 },
                new MenuItem { ItemName = "Bigos", Description = "Traditional Polish stew made with meat and sauerkraut", PhotoUrl = null, Ingredients = "Pork, beef, sauerkraut, onion, mushrooms, spices", Available = true, Price = 28.50m, CategoryId = 2, RestaurantId = 1 },
                new MenuItem { ItemName = "Placki ziemniaczane", Description = "Crispy potato pancakes served with sour cream and apple sauce", PhotoUrl = null, Ingredients = "Potatoes, onion, egg, flour, salt, pepper, oil", Available = true, Price = 16.50m, CategoryId = 2, RestaurantId = 1 },
                new MenuItem { ItemName = "Kotlet schabowy", Description = "Classic Polish pork cutlet served with potatoes and sauerkraut", PhotoUrl = null, Ingredients = "Pork loin, breadcrumbs, egg, potatoes, sauerkraut, butter, flour", Available = true, Price = 32.50m, CategoryId = 2, RestaurantId = 3 },
                new MenuItem { ItemName = "Pierogi z mięsem", Description = "Handmade dumplings filled with meat and served with fried onions and sour cream", PhotoUrl = null, Ingredients = "Flour, egg, water, ground beef, onion, spices, butter, sour cream", Available = true, Price = 24.50m, CategoryId = 2, RestaurantId = 2 },
                new MenuItem { ItemName = "Żurek", Description = "Traditional Polish sour soup made with fermented rye flour, sausage, and boiled egg", PhotoUrl = null, Ingredients = "Fermented rye flour, sausage, boiled egg, potato, onion, garlic, marjoram", Available = true, Price = 14.50m, CategoryId = 8, RestaurantId = 1 },
            };
            
            context.MenuItems.AddRange(menuitems);
            context.SaveChanges();
        }
    }
}