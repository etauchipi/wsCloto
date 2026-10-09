# wsCloto - Servicio Web de Citas Médicas

Este repositorio contiene `wsCloto`, un servicio web SOAP (WCF) diseñado para la gestión y procesamiento de citas médicas. El proyecto actúa como middleware, comunicándose con una base de datos mediante ODBC para consultar información de citas de usuarios y marcar citas como procesadas según parámetros específicos.

## 🛠 Pila Tecnológica (Tech Stack)

Basado en el código fuente, la pila tecnológica identificada es:
- **Lenguaje:** Visual Basic .NET (VB.NET)
- **Framework:** .NET Framework 4.5.2
- **Tipo de Proyecto:** Windows Communication Foundation (WCF)
- **Servidor Web / Hosting:** IIS (Internet Information Services) / IIS Express
- **Base de Datos / Conexión:** System.Data.Odbc (específicamente, conexión con bases de datos Informix/SQL mediante la librería externa `SQLIfxOdbcConnect`)
- **Dependencias (Bibliotecas de Clases):** `AppGeneral`, `AppUtils`, `Compresion`, `SQLIfxOdbcConnect`

## ⚙️ Instalación Local y Configuración del Entorno

Sigue estos pasos para configurar el proyecto en tu entorno local:

### 1. Prerrequisitos
- **Visual Studio 2019 o superior** (se recomienda instalar la carga de trabajo "Desarrollo de ASP.NET y web").
- **.NET Framework 4.5.2** instalado.
- Dependiendo de la base de datos a la que te vayas a conectar, asegúrate de tener instalados y configurados los **Drivers ODBC** (ej. Informix ODBC Driver) en tu máquina local.

### 2. Clonar el repositorio
Abre una terminal y clona este proyecto:
```bash
git clone <URL_DEL_REPOSITORIO>
cd wsCloto
```

### 3. Configuración de Variables (Web.config)
Abre el archivo `Web.config` y configura las cadenas de conexión (connectionStrings) y otros ajustes necesarios de tu entorno (como `FormatoFecha`).

Ejemplo para la cadena de conexión de prueba o producción (reemplaza `DSN=dsn` por el nombre de origen de datos correspondiente en tu equipo):
```xml
<connectionStrings>
    <!-- Configura tu DSN (Data Source Name) de ODBC aquí -->
    <add name="ApplicationServices_wsCloto" connectionString="DSN=MiDSNLocal;UID=usuario;PWD=contraseña;" providerName="System.Data.Odbc"/>
</connectionStrings>
```

### 4. Compilar y Ejecutar
1. Abre el archivo `wsCloto.sln` con Visual Studio.
2. Asegúrate de que las referencias en la carpeta `App_Code` (`AppGeneral.dll`, `AppUtils.dll`, `Compresion.dll`, `SQLIfxOdbcConnect.dll`) estén correctamente enlazadas al proyecto si marca algún error en la exploración de soluciones.
3. Haz clic en **Iniciar depuración (F5)** o en el botón de **IIS Express**. Visual Studio abrirá automáticamente el navegador con el WCF Test Client o la vista del servicio (`http://localhost:65236/wsCloto.svc`).

## 📁 Estructura Principal del Repositorio

- **`wsCloto.sln` / `wsCloto.vbproj`**: Archivos de solución y proyecto de Visual Studio.
- **`wsCloto.svc` / `wsCloto.svc.vb`**: Archivos que definen la implementación principal del servicio WCF. `wsCloto.svc.vb` contiene la lógica de negocio (consultas SQL `SELECT` y `UPDATE` con ODBC).
- **`IwsCloto.vb`**: La interfaz (`ServiceContract`) que define los métodos que expone este servicio web, junto con los contratos de datos (DataContracts) para las serializaciones SOAP (como `ws_CAutServiciosSalud`, etc.).
- **`Web.config`**: Archivo de configuración global de la aplicación (cadenas de conexión, bindings de WCF, comportamientos HTTP/HTTPS).
- **`App_Code/`**: Directorio que contiene las librerías dinámicas (`.dll`) de las que depende el servicio para utilidades generales, compresión y conexión especializada ODBC.

## 🚀 Guía Básica de Uso

Una vez que el servicio WCF está corriendo, se puede interactuar con él usando clientes SOAP (como SOAP UI, Postman, o WCF Test Client de Visual Studio). El servicio expone las siguientes operaciones principales:

### `Get_citas_usuario()`
Consulta la información de citas pendientes o activas. No recibe parámetros.
- **Retorno:** Un objeto `String` comprimido (utilizando la biblioteca `Compresion`) que internamente representa un `DataSet` con datos de pacientes, médicos, fechas e información de especialidad.

### `Set_citas_procesado(sCitdoc, sCitfue, sCitead, sObservacion, sNumero, sPrograma)`
Actualiza el estado de una cita en el sistema para marcarla como procesada.
- **Parámetros Requeridos:**
  - `sCitdoc`, `sCitfue`, `sCitead`: Identificadores únicos de la cita en la base de datos de origen.
  - `sObservacion`: Alguna nota u observación adicional.
  - `sNumero`: Número identificador del registro o trámite; determina el nuevo estado de la cita.
  - `sPrograma`: Información adicional sobre el programa de atención.
- **Retorno:** Un valor `Boolean` (`true` si la base de datos se actualizó correctamente, `false` en caso de error).
