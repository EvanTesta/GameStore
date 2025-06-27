using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Get Connection String From appsettings.json
// Adding Connection to Database
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

// Give Permissions To Front End
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowBlazor",
    builder => builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader());
});

var app = builder.Build();

app.MapGamesEndPoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();


app.Run();
