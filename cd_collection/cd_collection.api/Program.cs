using cd_collection.Repositories;
using cd_collection.Repositories.Contracts;
using cd_collection.Repository;
using cd_collection.Services;
using cd_collection.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IInMemoryItemsRepository, InMemoryItemsRepository>();
builder.Services.AddSingleton<IInMemoryCollectionRepository, InMemoryCollectionRepository>();

builder.Services.AddScoped<IItemsService, ItemsService>();
builder.Services.AddScoped<ICollectionsService, CollectionsService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();


