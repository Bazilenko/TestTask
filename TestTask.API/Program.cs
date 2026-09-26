using TestTask.DAL;
using TestTask.DAL.Interfaces;
using TestTask.DAL.Repositories;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDal(builder.Configuration);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.Run();

