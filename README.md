Film App
This project is a film-watching application that allows users to browse a film list, search for films, and save films they wish to watch. The application interacts with clients (frontend or mobile applications) via a web API.

Technologies

C# (.NET Core)
Web API (ASP.NET Core Web API)
Database (SQL Server)
ORM (Entity Framework)
Layered Architecture

Database Objects Layer: Defines objects to be saved to or read from the database.

Data Access Layer: Handles database access operations. Includes CRUD (Create, Read, Update, Delete) operations.
Business Logic Layer: Contains application business logic (movie search, adding movies, saving watched movies, etc.).

Web API Layer: Processes user requests and returns the relevant data.

API Endpoints

Authentication
POST /api/auth/register: Creates a new user account.
POST /api/auth/login: Logs in a user.
POST /api/auth/refresh: Refreshes authentication information.

Films
GET /api/films: Lists all films.
GET /api/films/{id}: Retrieves a specific film.
DELETE /api/films/{id}: Deletes a specific film.
POST /api/films: Creates a new film.
PATCH /api/films/{id}: Partially updates a specific film.
PUT /api/films/{id}: Updates a specific film.

Reviews
POST /api/reviews: Adds a film review.
GET /api/reviews/films/{filmId}: Retrieves reviews for a specific film.

Searches
GET /api/search: Performs a film search.

Users
GET /api/users: Lists all users.
GET /api/users/{id}: Retrieves a specific user.
GET /api/users/{id}/reviews: Retrieves reviews by a specific user.
DELETE /api/users/{id}: Deletes a specific user.
PUT /api/users/{id}: Updates a user by admin.
PUT /api/users: Updates a user.
POST /api/users/register: Creates a new user account.

WatchedFilms
POST /api/watched-films: Adds watched films.
GET /api/watched-films: Lists watched films.
DELETE /api/watched-films/{id}: Deletes a watched film.

WatchList
POST /api/watchlist: Adds a film to the watchlist.
GET /api/watchlist: Retrieves the watchlist.
DELETE /api/watchlist/{id}: Removes a film from the watchlist.
