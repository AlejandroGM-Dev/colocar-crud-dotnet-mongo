===========================================================

&nbsp;     COLOCAR CRUD (.NET 9 + MONGODB)

===========================================================



CRUD sencillo de Usuarios construido con .NET 9 y MongoDB, 

utilizando una arquitectura por capas y ASP.NET Core MVC.



-----------------------------------------------------------

1\. FUNCIONALIDAD

-----------------------------------------------------------

\* Crear usuario

\* Listar usuarios

\* Editar usuario

\* Eliminar usuario

\* Prevención de duplicados por email (Índice único en Mongo)



-----------------------------------------------------------

2\. ARQUITECTURA DEL PROYECTO

-----------------------------------------------------------

El proyecto se divide en las siguientes capas:



\- ColocarCrud.Web: 

&nbsp; Presentación (MVC) -> Controladores y Vistas.



\- ColocarCrud.Application: 

&nbsp; Lógica de aplicación -> DTOs, Servicios y Validaciones.



\- ColocarCrud.Infrastructure: 

&nbsp; Persistencia -> MongoContext y Repositorios (MongoDB.Driver).



FLUJO: UI -> Controller -> Service -> Repository -> MongoDB



-----------------------------------------------------------

3\. REQUISITOS PREVIOS

-----------------------------------------------------------

\- .NET SDK 9 instalado.

\- Docker Desktop instalado y corriendo (Recomendado).

&nbsp; \*Nota: También funciona con MongoDB instalado localmente.



-----------------------------------------------------------

4\. EJECUCIÓN PASO A PASO (DOCKER)

-----------------------------------------------------------



PASO 1: Descargar el proyecto



Clona el repositorio desde la terminal:



\- git clone https://github.com/AlejandroGM-Dev/colocar-crud-dotnet-mongo.git

\- cd colocar-crud-dotnet-mongo



PASO 2: Levantar base de datos (Docker Desktop)



Ejecuta el siguiente comando para iniciar MongoDB:



docker-compose up -d



Para apagar MongoDB: docker compose down



PASO 3: Ejecutar la aplicación (Asegurarse estar en CMD ubicado en la raíz del proyecto)



Inicia el proyecto web:



dotnet run --project ColocarCrud.Web



PASO 4: Acceso



Abre tu navegador en: http://localhost:5000 (o el puerto indicado)



===========================================================

Desarrollado por: AlejandroGM-Dev

