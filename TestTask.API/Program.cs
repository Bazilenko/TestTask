using TestTask.DAL;
using TestTask.BLL;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDal(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseMiddleware<TestTask.API.Middlewares.ExceptionHandlingMiddleware>();

await app.Services.SeedDalAsync(
    CancellationToken.None);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

