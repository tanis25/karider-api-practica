# karider-api (práctica de despliegue)

API .NET 8 (Minimal API) de práctica para KARider. Se publica en **Azure App Service** con GitHub Actions.

## Local
```bash
dotnet run
# http://localhost:5000/swagger   y   http://localhost:5000/api/estado
```
Para una cadena de conexión local usa **user-secrets** (no se guarda en el repo):
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:KariderDb" "Host=localhost;Database=karider;Username=postgres;Password=..."
```

## Configuración en Azure (App Service → Settings → Environment variables)
| Pestaña | Nombre | Valor |
|---|---|---|
| App settings | `Cors__OrigenesPermitidos` | `https://<tu-sitio>.azurestaticapps.net,http://localhost:5173` |
| Connection strings | `KariderDb` (tipo **Custom**) | cadena de PostgreSQL |

Endpoints: `GET /api/estado`, `GET /swagger`.
