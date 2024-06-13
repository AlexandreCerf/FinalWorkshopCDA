using Microsoft.EntityFrameworkCore;
using WorkshopCDA.Data;
using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<FinalWorkshopContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.Parse("5.7.24-mysql")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseFastEndpoints();
app.MapControllers();

app.Run();
