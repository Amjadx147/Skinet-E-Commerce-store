using Core.InterFaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Middalwere;
using StackExchange.Redis;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddDbContext<StorContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaltConnection"));
    
});

builder.Services.AddScoped<IProductRepository , ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<> ), typeof(GenericRepository<>)) ;
builder.Services.AddCors(); 
builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
{
    var connstring = builder.Configuration.GetConnectionString("Redis") ?? throw new Exception("Cannot get redis connecation string");
    var configuration  = ConfigurationOptions.Parse(connstring, true);
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<ICartService, CartService>();

  

var app = builder.Build();

// Configure the HTTP request pipeline.




app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:4200","https://localhost:4200" ));

app.MapControllers();



try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StorContext>();
    await context.Database.MigrateAsync();
    await StoreContectSeed.SeedAsync(context);

}
catch (System.Exception ex)
{
    Console.WriteLine(ex);
    throw;
}
app.Run();

