# 📌 Notice Board — дошка оголошень

Веб-застосунок дошки оголошень на **ASP.NET Core 8**. Користувачі входять через **Google**, публікують оголошення з категоріями та підкатегоріями, редагують і видаляють свої оголошення та переглядають чужі.

Проєкт складається з двох частин: **REST API** (бекенд) та **MVC-сайту** (фронтенд), який звертається до API.

## Можливості

- **Вхід через Google** (OAuth 2.0) з видачею власного **JWT-токена**
- **Вихід із системи** з анулюванням токена (чорний список токенів у middleware)
- **Оголошення** — створення, редагування, видалення, перегляд деталей
- **«Мої оголошення»** — окрема сторінка з оголошеннями поточного користувача
- **Категорії та підкатегорії**
- **Статус оголошення** — активне / неактивне
- **Пагінація** списків оголошень
- Валідація даних і єдиний формат відповідей API (Result Pattern — `ServiceResponse`)

## Технології

- **C# / .NET 8**, ASP.NET Core Web API + ASP.NET Core MVC
- **Microsoft SQL Server** + **Dapper**, логіка вибірок — у **збережених процедурах**
- **JWT Bearer**, **Google OAuth** (`Google.Apis.Auth`)
- **Swagger**
- Docker

## Структура

```
database/
├── migrations/initial.sql        # таблиці: Users, Categories, SubCategories, Announcements
└── stored_procedures/            # sp_GetAnnouncements, sp_InsertAnnouncement, ...
NoticeBoardAPI/
├── NoticeBoardAPI/               # REST API
│   ├── Controllers/              # Announcements, Auth, Categories
│   ├── Services/, Repositories/  # бізнес-логіка та доступ до БД (Dapper)
│   ├── Middleware/               # перевірка анульованих токенів
│   └── DTOs/, Models/, Common/
└── NoticeBoardMVC/               # веб-інтерфейс
    ├── Controllers/, Views/
    └── Services/ApiService.cs    # HTTP-клієнт до API
```

## API

| Метод | Шлях | Опис |
|---|---|---|
| `POST` | `/api/auth/google` | вхід через Google, повертає JWT |
| `POST` | `/api/auth/logout` | вихід (анулювання токена) |
| `GET` | `/api/auth/me` | поточний користувач |
| `GET` | `/api/announcements?page=&pageSize=` | усі оголошення з пагінацією |
| `GET` | `/api/announcements/{id}` | одне оголошення |
| `GET` | `/api/announcements/my` | мої оголошення |
| `POST` | `/api/announcements` | створити |
| `PUT` | `/api/announcements/{id}` | редагувати |
| `DELETE` | `/api/announcements/{id}` | видалити |
| `GET` | `/api/categories` | категорії |
| `GET` | `/api/categories/{id}/subcategories` | підкатегорії категорії |

## Запуск

**Потрібно:** .NET 8 SDK, SQL Server, Google OAuth Client ID ([Google Cloud Console](https://console.cloud.google.com/apis/credentials)).

1. Створіть базу `notice_board` і виконайте скрипти з папки `database/`: спочатку `migrations/initial.sql`, потім усі файли з `stored_procedures/`.
2. Скопіюйте `appsettings.example.json` → `appsettings.json` у проєктах `NoticeBoardAPI` та `NoticeBoardMVC` і заповніть:
   - `ConnectionStrings:DefaultConnection`
   - `GoogleAuth:ClientId` / `ClientSecret`
   - `JwtSettings:SecretKey` (мінімум 32 символи, однаковий в обох проєктах)
   - `ApiSettings:BaseUrl` у MVC — адреса запущеного API
3. Запустіть обидва проєкти:
   ```bash
   dotnet run --project NoticeBoardAPI/NoticeBoardAPI
   dotnet run --project NoticeBoardAPI/NoticeBoardMVC
   ```
