using Microsoft.EntityFrameworkCore;
using SE172788.ProductManagement.Repo.Data;
using SE172788.ProductManagement.Repo.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext with the connection string from environment variable
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ConnectionstringAPI");
    options.UseSqlServer(connectionString);
});

// Register UnitOfWork and Repositories
builder.Services.AddScoped<UnitOfWork>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
