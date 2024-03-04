using cd_collection.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CollectionsInMemoryRepository>();
builder.Services.AddScoped<ICollectionsRepository, CollectionsInMemoryRepository>();
builder.Services.AddScoped<IItemsRepository, ItemsInMemoryRepository>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();


