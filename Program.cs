    using EmployeeApi.Data;
    using Microsoft.EntityFrameworkCore;
    using  EmployeeApi.Services;
    using EmployeeApi.Repositories;
    
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(); 
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlite("Data Source=employees.db"));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
var app = builder.Build();
app.UseExceptionHandler(errorApp =>
{   
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices
            .GetRequiredService<ILogger<Program>>();

        logger.LogError("An unexpected error occurred.");

        context.Response.StatusCode = 500;

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Something went wrong. Please try again later."
        });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
;


