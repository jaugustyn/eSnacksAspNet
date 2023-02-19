# eSnacks
Aplikacja webowa umożliwiająca zamawianie jedzenia z różnych restauracji. Aplikacja jest napisana w ASP.NET MVC .NET 6 przy użyciu Entity Frameworka i SQLite.

### Wymagania
Aplikacja wymaga zainstalowania następujących narzędzi:
```
- .NET 6
- Visual Studio lub Visual Studio Code
```
## Instalacja i uruchomienie
Sklonuj repozytorium.
```
https://github.com/jaugustyn/eSnacksMVC.git
```
Otwórz projekt w Visual Studio lub Visual Studio Code.
Otwórz konsolę menedżera pakietów i uruchom poniższe polecenia w celu utworzenia bazy danych.
```
Dotnet restore
Dotnet build
Add-Migration Init
Update-Database
```

Uruchom aplikację.

### Seedowane dane
Aplikacja ma seedowane dane, w tym dwóch użytkowników. Poniżej przedstawiam adresy e-mail oraz hasła tych użytkowników:
```
User
Email: user@eSnacks.com
Hasło: zaq1@WSX

Admin
Email: admin@eSnacks.com
Hasło: zaq1@WSX
```
Te dane mogą być wykorzystane do zalogowania się w aplikacji i przetestowania jej funkcjonalności.
Jednakże, w celach bezpieczeństwa, zaleca się zmianę hasła tych użytkowników po zalogowaniu się do aplikacji.

## Opis
### Modele
- `eSnacksUser:` model użytkownika, rozszerza model IdentityUser o pola dotyczące imienia, nazwiska, adresu i daty urodzenia oraz kolekcję zamówień.
- `Category:` model kategorii, zawiera nazwę kategorii, opis i kolekcję elementów menu.
- `City:` model miasta, zawiera nazwę miasta, kod pocztowy i kolekcję restauracji.
- `Restaurant:` model widoku restauracji, zawiera nazwę, adres, zdjęcie, opis, identyfikator miasta.
- `MenuItem:` model widoku nowego elementu menu, zawiera nazwę, opis, zdjęcie, składniki, cenę, identyfikator kategorii i identyfikator restauracji.
- `InOrder:` model zamówienia, zawiera ilość, cenę, komentarz, identyfikator zamówienia i identyfikator elementu menu.
- `PlacedOrder:` model złożonego zamówienia, zawiera datę złożenia zamówienia, czas szacowany na dostawę, adres dostawy oraz kolekcję elementów zamówienia.
- `OrderStatus:` model reprezentuje status zamówienia, zawiera pole Status, które określa nazwę statusu. Jest powiązany z PlacedOrders

### Opis funkcjonalności
- Rejestracja użytkowników i logowanie
- Wybór miasta i wyświetlanie listy restauracji w danym mieście
- Wybór restauracji i wyświetlanie menu
- Dodawanie elementów menu do koszyka zakupowego
- Przeglądanie i modyfikacja koszyka zakupowego
- Składanie zamówienia
- Przeglądanie historii zamówień
- Zarządzanie profilem użytkownika (zmiana hasła, adresu, numeru telefonu itp.)
