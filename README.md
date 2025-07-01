# MovieAPI - Plataforma de Películas

Una API completa de películas desarrollada en C# con ASP.NET Core, inspirada en el diseño de Prime Video. Incluye autenticación JWT, gestión de favoritos, historial de visualización y un frontend moderno.

## 🎬 Características

- **API RESTful completa** con ASP.NET Core 8.0
- **Autenticación JWT** con roles de usuario y administrador
- **Base de datos SQL Server** con Entity Framework Core
- **Gestión de películas** con CRUD completo
- **Sistema de favoritos** para usuarios
- **Historial de visualización**
- **Búsqueda avanzada** por título, director, género, etc.
- **Frontend moderno** con diseño similar a Prime Video
- **Responsive design** para móviles y tablets
- **AutoMapper** para mapeo de objetos
- **Swagger/OpenAPI** para documentación

## 🚀 Tecnologías Utilizadas

### Backend
- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **SQL Server LocalDB**
- **JWT Authentication**
- **AutoMapper**
- **BCrypt** para hash de contraseñas

### Frontend
- **HTML5, CSS3, JavaScript**
- **Font Awesome** para iconos
- **Diseño responsive**
- **Modales y animaciones**

## 📋 Requisitos Previos

- **.NET 8.0 SDK** o superior
- **SQL Server LocalDB** (incluido con Visual Studio)
- **Visual Studio 2022** o **Visual Studio Code**

## 🛠️ Instalación

### 1. Clonar el repositorio
```bash
git clone <url-del-repositorio>
cd MovieAPI
```

### 2. Restaurar dependencias
```bash
dotnet restore
```

### 3. Configurar la base de datos
La aplicación utiliza SQL Server LocalDB por defecto. La cadena de conexión está configurada en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
}
```

### 4. Ejecutar las migraciones
```bash
dotnet ef database update
```

### 5. Ejecutar la aplicación
```bash
dotnet run
```

La API estará disponible en: `https://localhost:7001`
El frontend estará disponible en: `https://localhost:7001`

## 📚 Documentación de la API

### Autenticación

#### Registro de Usuario
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "usuario",
  "email": "usuario@email.com",
  "password": "contraseña123",
  "firstName": "Nombre",
  "lastName": "Apellido",
  "dateOfBirth": "1990-01-01"
}
```

#### Inicio de Sesión
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "usuario",
  "password": "contraseña123"
}
```

### Películas

#### Obtener todas las películas
```http
GET /api/movies
```

#### Obtener películas destacadas
```http
GET /api/movies/featured
```

#### Obtener películas nuevas
```http
GET /api/movies/new
```

#### Buscar películas
```http
GET /api/movies/search?q=batman
```

#### Obtener película por ID
```http
GET /api/movies/{id}
```

#### Crear película (Admin)
```http
POST /api/movies
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Nueva Película",
  "description": "Descripción de la película",
  "year": 2024,
  "genre": "Acción",
  "director": "Director",
  "cast": "Actor 1, Actor 2",
  "rating": 8.5,
  "posterUrl": "https://ejemplo.com/poster.jpg",
  "trailerUrl": "https://youtube.com/watch?v=...",
  "videoUrl": "https://ejemplo.com/video.mp4",
  "duration": 120,
  "language": "Español",
  "isFeatured": false,
  "isNew": true
}
```

#### Actualizar película (Admin)
```http
PUT /api/movies/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Título Actualizado",
  "rating": 9.0
}
```

#### Eliminar película (Admin)
```http
DELETE /api/movies/{id}
Authorization: Bearer {token}
```

### Favoritos

#### Agregar a favoritos
```http
POST /api/movies/{id}/favorites
Authorization: Bearer {token}
```

#### Remover de favoritos
```http
DELETE /api/movies/{id}/favorites
Authorization: Bearer {token}
```

#### Obtener favoritos del usuario
```http
GET /api/movies/favorites
Authorization: Bearer {token}
```

### Historial de Visualización

#### Agregar al historial
```http
POST /api/movies/{id}/watch
Authorization: Bearer {token}
Content-Type: application/json

0
```

#### Obtener historial del usuario
```http
GET /api/movies/history
Authorization: Bearer {token}
```

### Usuarios

#### Obtener perfil del usuario
```http
GET /api/users/{id}
Authorization: Bearer {token}
```

#### Actualizar perfil
```http
PUT /api/users/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "firstName": "Nuevo Nombre",
  "lastName": "Nuevo Apellido"
}
```

## 👤 Usuarios por Defecto

La aplicación incluye un usuario administrador por defecto:

- **Usuario:** `admin`
- **Contraseña:** `admin123`
- **Email:** `admin@movieapi.com`
- **Rol:** `Admin`

## 🎨 Características del Frontend

### Diseño
- **Tema oscuro** similar a Prime Video
- **Gradientes y efectos visuales** modernos
- **Animaciones suaves** y transiciones
- **Diseño responsive** para todos los dispositivos

### Funcionalidades
- **Navegación intuitiva** entre secciones
- **Búsqueda en tiempo real**
- **Sistema de favoritos** con iconos interactivos
- **Modales** para detalles de películas
- **Formularios de autenticación** estilizados
- **Indicadores de carga** y estados

### Secciones Disponibles
- **Inicio:** Todas las películas
- **Destacadas:** Películas marcadas como destacadas
- **Nuevas:** Películas marcadas como nuevas
- **Favoritos:** Películas favoritas del usuario (requiere autenticación)
- **Historial:** Historial de visualización (requiere autenticación)

## 🔧 Configuración

### Variables de Entorno
Puedes configurar las siguientes variables en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "tu-cadena-de-conexion"
  },
  "Jwt": {
    "Key": "tu-clave-secreta-jwt",
    "Issuer": "MovieAPI",
    "Audience": "MovieAPI_Users",
    "ExpiryInMinutes": 60
  }
}
```

### CORS
La aplicación está configurada para permitir todas las origenes en desarrollo. Para producción, configura las políticas CORS apropiadas.

## 🧪 Testing

### Swagger UI
Accede a la documentación interactiva de la API en:
```
https://localhost:7001/swagger
```

### Datos de Prueba
La aplicación incluye 10 películas de ejemplo con datos reales:
- El Señor de los Anillos: La Comunidad del Anillo
- Inception
- Pulp Fiction
- The Dark Knight
- Fight Club
- Forrest Gump
- The Matrix
- Goodfellas
- Interstellar
- The Silence of the Lambs

## 🚀 Despliegue

### Local
```bash
dotnet run
```

### Producción
```bash
dotnet publish -c Release
dotnet MovieAPI.dll
```

## 📝 Estructura del Proyecto

```
MovieAPI/
├── Controllers/          # Controladores de la API
├── Data/                # Contexto de base de datos y seed data
├── DTOs/                # Data Transfer Objects
├── Models/              # Modelos de entidad
├── Services/            # Lógica de negocio
├── Mappings/            # Configuración de AutoMapper
├── wwwroot/             # Archivos estáticos del frontend
├── Program.cs           # Configuración de la aplicación
├── appsettings.json     # Configuración
└── README.md           # Este archivo
```

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo `LICENSE` para más detalles.

## 🆘 Soporte

Si tienes problemas o preguntas:

1. Revisa la documentación de Swagger en `/swagger`
2. Verifica los logs de la aplicación
3. Asegúrate de que SQL Server LocalDB esté instalado y funcionando
4. Comprueba que todas las dependencias estén instaladas

## 🎯 Próximas Características

- [ ] Sistema de calificaciones de usuarios
- [ ] Recomendaciones personalizadas
- [ ] Subtítulos y múltiples idiomas
- [ ] Sistema de comentarios y reseñas
- [ ] Integración con APIs externas (TMDB, OMDB)
- [ ] Notificaciones push
- [ ] Modo offline
- [ ] Tests unitarios y de integración 