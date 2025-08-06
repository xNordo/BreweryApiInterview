# Brewery API Application

## Struktura projektu

Projekt został zorganizowany zgodnie z zasadami Clean Architecture, z wyraźnym podziałem na warstwy:

### Warstwy aplikacji

- **API** - Zawiera kontrolery, modele DTO i mappery do komunikacji z klientami
- **Application** - Zawiera logikę biznesową, serwisy aplikacyjne, interfejsy repozytoriów
- **Domain** - Zawiera encje domenowe
- **Infrastructure** - Zawiera implementacje repozytoriów, usługi zewnętrzne i cache

### Kluczowe komponenty

- **BreweriesController** - Główny kontroler API obsługujący zapytania o browary
- **BreweryService** - Serwis aplikacyjny dostarczający logikę biznesową
- **BreweryRepository** - Repozytorium pobierające dane z zewnętrznego API i przechowujące je w cache
- **MemoryCacheService** - Implementacja cache w pamięci
- **BreweryApiService** - Usługa komunikująca się z zewnętrznym API browarów

## Decyzje projektowe


### Obsługa cache

- Implementacja cache w pamięci dla optymalizacji zapytań
- Generowanie unikalnych kluczy cache na podstawie parametrów zapytania

### Wersjonowanie API

- Implementacja wersjonowania w URL (np. /api/v1/breweries)
- Wsparcie dla wersjonowania przez nagłówki HTTP

## Uruchamianie aplikacji

### Wymagania

- .NET 9.0 SDK
- IDE (Visual Studio, Rider, VS Code)

### Instrukcje uruchomienia

1. Sklonuj repozytorium
2. Otwórz projekt w wybranym IDE
3. Uruchom aplikację:

```bash
dotnet run --project BreweryApiInterview
```

Aplikacja będzie dostępna pod adresem: http://localhost:5098

### Endpointy API

- `GET /api/v1/breweries` - Pobieranie listy browarów z opcjonalnymi parametrami filtrowania i sortowania

#### Dostępne parametry

- `search` - Wyszukiwanie ogólne
- `byCity` - Filtrowanie po mieście
- `byName` - Filtrowanie po nazwie
- `sortBy` - Sortowanie wyników (name, type, city)
- `sortDirection` - Kierunek sortowania (ascending, descending)
- `page` - Numer strony (domyślnie 1)
- `perPage` - Liczba elementów na stronie (domyślnie 20, maksymalnie 50)

