## Instrucciones para ejecutar el app

Antes de ejecutar la aplicaci�n, es necesario hacer lo siguiente:

### 1. Correr el Script de la Base de Datos

Debe crear la base de datos y tablas requeridas ejecutando el script SQL que se encuentra en `SinpeEmpresarial.Web/sql/SinpeEmpresarialDB.sql`.


**C�mo ejecutar el script:**
- Con�ctarse al SQL Server.
- Abrir el archivo `SinpeEmpresarialDB.sql` desde la ruta indicada arriba.
- Ejecutar el script para crear la base de datos y todas las tablas necesarias.

### 2. Actualizar el Connection String en Web.config

El valor de `Data Source` en el connection string de la aplicacion debe ser actualizado para que coincida con el nombre del server usado en la seccion anterior.

**Ubicaci�n:**  

El connection string est� en `SinpeEmpresarial.Web\Web.config`. Reemplazar `NOMBRE_DEL_SERVER` con el nombre del SQL Server que se est� utilizando.
**Ejemplo:**
```xml
  <connectionStrings>
    <add name="SinpeDbContext" connectionString="Data Source=NOMBRE_DEL_SERVER;Initial Catalog=SinpeEmpresarialDB;Integrated Security=True;encrypt=False" providerName="System.Data.SqlClient" />
  </connectionStrings>