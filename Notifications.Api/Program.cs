using Notifications.Api.Configurations.Modules;
using Notifications.Api.Middleware;
using Notifications.API.Configurations;
using Notifications.Infraestruture.Correlation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEmailOptions(builder.Configuration);
builder.Services.RegisterServices();
// Add services to the container.


builder.Services.AddScoped<ICorrelationIdSentinel, CorrelationIdSentinel>();
//builder.Services.AddHttpClient<IModerna, Moderna>((o,p) =>
//{
//    var corr = p.GetRequiredService<ICorrelationIdSentinel>();

//    o.DefaultRequestHeaders.Add(corr.GetHeaderName(), corr.Get());

//    return new Moderna(o);
//});


builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();
