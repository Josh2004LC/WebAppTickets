# WebAppTickets — Sistema de Gestión de Tickets

Aplicación web desarrollada con **ASP.NET Core MVC y C#** para gestionar tickets de soporte e incidencias. El sistema permite registrar, consultar, modificar y administrar tickets mediante una interfaz web conectada a una API REST.

## Tecnologías utilizadas

- C# y .NET 8
- ASP.NET Core MVC
- Razor Views
- Bootstrap
- HttpClient
- Autenticación mediante cookies
- API REST

## Funcionalidades

- Inicio de sesión de usuarios.
- Visualización del listado de tickets.
- Registro de nuevos tickets.
- Consulta de detalles.
- Edición y eliminación de tickets.
- Asignación de categorías, urgencias e importancias.
- Asociación de tickets con usuarios.
- Acceso restringido a las funciones de tickets mediante autenticación.

## Estructura del proyecto

- `Controllers`: controladores de usuarios, tickets y navegación.
- `Models`: modelos utilizados por la aplicación.
- `Services`: servicios encargados de comunicarse con la API.
- `Views`: interfaces desarrolladas con Razor.

## Instalación

1. Clonar el repositorio.
2. Instalar el SDK de .NET 8.
3. Configurar y ejecutar el backend WebAPITickets.
4. Ajustar `ApiSettings:BaseUrl` en `appsettings.json` para apuntar a la API.
5. Ejecutar los siguientes comandos:

```bash
dotnet restore
dotnet run
```

## Capturas de pantalla

*Pendiente: agregar capturas reales de la aplicación.*

## Mejoras futuras

- Incorporar pruebas automatizadas.
- Mejorar la validación de formularios.
- Fortalecer la seguridad y autorización de usuarios.
- Optimizar la experiencia de usuario.

## Backend

[WebAPITickets](https://github.com/Josh2004LC/WebAPITickets)

## Estado del proyecto

Proyecto académico en proceso de mejoras, desarrollado para aplicar conocimientos de programación web, arquitectura MVC e integración con servicios REST.
