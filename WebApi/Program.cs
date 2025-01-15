using Microsoft.EntityFrameworkCore;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 

builder.Services.AddOpenApi();

var app = builder.Build();




app.UseHttpsRedirection();

app.MapControllers();

app.Run();

