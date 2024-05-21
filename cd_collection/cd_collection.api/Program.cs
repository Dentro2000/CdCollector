using cd_collection.application;
using cd_collection.core;
using cd_collection.infrastructure;
using cd_collection.infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddCore();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseInfrastructure();
app.MapControllers();
app.Run();