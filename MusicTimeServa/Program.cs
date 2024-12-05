using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using MusicTimeServa.Model;
using MusicTimeServa.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorLocalhost", policy =>
    {
        policy.WithOrigins("https://localhost:7007")  // The origin of your Blazor app
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEntrieService, EntryService>();

var app = builder.Build();
app.UseCors("AllowBlazorLocalhost");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
