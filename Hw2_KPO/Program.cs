//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Hw2_KPO.Application.Events;
using Hw2_KPO.Application.Services;
using Hw2_KPO.Domain.Interfaces;
using Hw2_KPO.Infrastructure.Data;
using Hw2_KPO.Infrastructure.Repositories;
using Hw2_KPO.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Beelzebufo's Zoo Management API", Version = "v1" });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


builder.Services.AddSingleton<InMemoryZooDatabase>();

builder.Services.AddScoped<IAnimalRepository, InMemoryAnimalRepository>();
builder.Services.AddScoped<IEnclosureRepository, InMemoryEnclosureRepository>();
builder.Services.AddScoped<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

builder.Services.AddScoped<AnimalTransferService>();
builder.Services.AddScoped<FeedingService>();
builder.Services.AddScoped<ZooStatisticsService>();

builder.Services.AddSingleton<EventHandlerService>();
builder.Services.AddScoped<IDomainEventHandler<AnimalMovedEvent>, AnimalMovedEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<FeedingTimeEvent>, FeedingTimeEventHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Beelzebufo's Zoo Management API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();