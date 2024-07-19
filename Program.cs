using AdminHRM.Server.AppSettings;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Infrastructures;
using AdminHRM.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// // Load PostgreSQL settings
var postgreSetting = new PostgreSetting();
builder.Configuration.Bind("PostgreSetting", postgreSetting);
builder.Services.AddSingleton(postgreSetting);
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policyOption =>
    {
        policyOption.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
    });
});

builder.Services.AddApplicationServicesExtension();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HrmDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IEmployeeServive, EmployeeServive>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Configure Entity Framework to use PostgreSQL
builder.Services.AddDbContext<HrmDbContext>(options =>
    options.UseNpgsql(postgreSetting.ConnectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();