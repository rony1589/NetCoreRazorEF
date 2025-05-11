# 📚 SchoolManagement
SchoolManagement es una aplicación web desarrollada con ASP.NET Core y Razor Pages que permite gestionar estudiantes, materias e inscripciones. Sigue una arquitectura por capas para promover la separación de responsabilidades y facilitar el mantenimiento y escalabilidad del sistema.

## 🏗️ Arquitectura del Proyecto
La solución está organizada en los siguientes proyectos:

- **School.Core**: Contiene las entidades del dominio y las interfaces de los servicios.

- **School.Application**: Incluye los DTOs y las configuraciones de mapeo entre entidades y DTOs.

- **School.Infrastructure**: Implementa los servicios definidos en School.Core y maneja la interacción con la base de datos mediante Entity Framework Core.

- **School.Web**: Proyecto principal de la aplicación web que utiliza Razor Pages para la interfaz de usuario.

- **School.Tests**: Contiene las pruebas unitarias para los servicios y controladores.


## 📁 Estructura de Carpetas
# 🏫 SchoolManagement - Estructura del Proyecto

```text
SchoolManagement/
├── 📂 School.Core/                   # Capa de dominio
│   ├── 📂 Entities/
│   │   ├── 🟢 Student.cs
│   │   ├── 🟢 Subject.cs
│   │   └── 🟢 Enrollment.cs
│   └── 📂 Interfaces/
│       ├── 🔷 IStudentService.cs
│       ├── 🔷 ISubjectService.cs
│       └── 🔷 IEnrollmentService.cs
│
├── 📂 School.Application/            # Capa de aplicación
│   ├── 📂 DTOs/
│   │   ├── 📄 EnrollmentDto.cs
│   │   ├── 📄 StudentDto.cs
│   │   └── 📄 SubjectDto.cs
│   └── 📂 Mapping/
│       └── 📄 MappingProfile.cs
│
├── 📂 School.Infrastructure/         # Infraestructura
│   ├── 📂 Data/
│   │   └── 📄 SchoolDbContext.cs
│   ├── 📂 Services/
│   │   ├── 🛠️ StudentService.cs
│   │   ├── 🛠️ SubjectService.cs
│   │   └── 🛠️ EnrollmentService.cs
│   └── 📄 DependencyInjection.cs
│
├── 📂 School.Tests/                  # Pruebas unitarias
│   ├── 📂 Controllers/
│   │   └── 🧪 StudentControllerTests.cs
│   ├── 📂 Services/
│   │   └── 🧪 StudentServiceTests.cs
│   └── 📄 BasePruebas.cs
│
├── 📂 School.Web/                    # Frontend (Razor Pages)
│   ├── 📂 Pages/
│   │   ├── 📂 Students/
│   │   │   ├── 📄 Index.cshtml + Index.cshtml.cs
│   │   │   ├── 📄 Create.cshtml + Create.cshtml.cs
│   │   │   ├── 📄 Edit.cshtml + Edit.cshtml.cs
│   │   │   └── 📄 Delete.cshtml + Delete.cshtml.cs
│   │   ├── 📂 Subjects/
│   │   └── 📂 Enrollments/
│   ├── 📂 wwwroot/                   # Archivos estáticos
│   └── 📄 Program.cs                 # Configuración inicial
│
└── 📄 SchoolManagement.sln           # Solución principal
```

## 🚀 Requisitos Previos
Antes de ejecutar el proyecto, asegúrate de tener instalado lo siguiente:
- Visual Studio 2022 con el paquete de desarrollo de ASP.NET y desarrollo de bases de datos.
- SQL Server 2019 o superior.
- .NET 6 SDK o superior.

## 🚀 Instalación y Ejecución

## 1. 📥 Clonación del Repositorio
Para clonar el repositorio, ejecuta el siguiente comando en tu terminal:
```bash
git clone https://github.com/rony1589/NetCoreRazorEF.git
cd NetCoreRazorEF
```

## 2. Restaurar los paquetes NuGet
dotnet restore


## 3. ⚙️ Configuración de la Conexión a la Base de Datos
La cadena de conexión a la base de datos se encuentra en el archivo appsettings.json dentro del proyecto School.Web.

📁 appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=SchoolDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

Reemplaza TU_SERVIDOR con el nombre de tu instancia de SQL Server. Si estás utilizando SQL Server Express, podría ser localhost\\SQLEXPRESS.

## 🧪 Migraciones y Creación de la Base de Datos
Para aplicar las migraciones y crear la base de datos, utiliza la Consola del Administrador de Paquetes en Visual Studio:

Add-Migration InitialCreate
Update-Database

o

dotnet ef database update --project School.Infrastructure

Esto generará las tablas necesarias en la base de datos especificada.

## ▶️ Ejecución del Proyecto
Para ejecutar la aplicación:

1. Abre la solución SchoolManagement.sln en Visual Studio.

2. Establece School.Web como proyecto de inicio.

3. Presiona F5 o haz clic en "Iniciar" para ejecutar la aplicación.

4. La aplicación se abrirá en tu navegador predeterminado mostrando la lista de estudiantes.

## ✅ Pruebas Unitarias
El proyecto School.Tests contiene pruebas unitarias para validar la lógica de negocio. Para ejecutarlas:

1. Abre el Explorador de Pruebas en Visual Studio (Prueba > Ventanas > Explorador de pruebas).

2. Haz clic en "Ejecutar todas" para ejecutar las pruebas.

o

dotnet test

## 📦 Publicación
Para publicar la aplicación en un servidor o servicio en la nube:

1. Haz clic derecho en el proyecto School.Web y selecciona Publicar.

2. Sigue el asistente para seleccionar el destino de publicación (por ejemplo, Azure, carpeta local, IIS, etc.).

## 📄 Licencia
Este proyecto está bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.
