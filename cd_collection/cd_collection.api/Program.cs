using cd_collection.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CollectionsService>();
builder.Services.AddSingleton<ICollectionsService, CollectionsService>();
builder.Services.AddSingleton<IItemsService, ItemsService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();


