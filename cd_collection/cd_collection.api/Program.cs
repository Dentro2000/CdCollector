using cd_collection.Repositories;
using cd_collection.Repositories.Contracts;
using cd_collection.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICollectionsService, CollectionsService>();
builder.Services.AddScoped<IItemsService, ItemsService>();
builder.Services.AddSingleton<IInMemoryCollectionRepository, InMemoryCollectionRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();


