using ServiceLifetimeDemo;
using ServiceLifetimeDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<ISingletonCounter, SingletonCounter>();
builder.Services.AddScoped<IScopedCounter, ScopedCounter>();
builder.Services.AddTransient<ITransientCounter, TransientCounter>();

var app = builder.Build();

// Enable serving of `index.html`
app.UseStaticFiles();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
