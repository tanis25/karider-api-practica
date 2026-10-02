var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// CORS: los orígenes permitidos NO se escriben en el código.
// En Azure se define la variable de entorno  Cors__OrigenesPermitidos
// con las URLs separadas por coma, por ejemplo:
//   https://<tu-sitio>.azurestaticapps.net,http://localhost:5173
// ---------------------------------------------------------------------------
var origenes = (builder.Configuration["Cors:OrigenesPermitidos"] ?? "http://localhost:5173")
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddCors(o => o.AddPolicy("Frontend", p =>
    p.WithOrigins(origenes).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger habilitado también en Azure para poder probar la API en la práctica.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("Frontend");

// ✏️ CAMBIO VISIBLE: edita este mensaje o la versión, haz commit y push a main
// y comprueba que /api/estado devuelve el valor nuevo.
const string Mensaje = "Hola desde la API de KARider";
const string Version = "1.0.0";

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapGet("/api/estado", (IConfiguration config, IWebHostEnvironment env) => new
{
    mensaje = Mensaje,
    version = Version,
    entorno = env.EnvironmentName,
    // Solo se informa SI existe la cadena de conexión; nunca se muestra su valor.
    cadenaConexionConfigurada = !string.IsNullOrWhiteSpace(config.GetConnectionString("KariderDb")),
    horaServidor = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
})
.WithName("Estado");

app.Run();
