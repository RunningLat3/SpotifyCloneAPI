global using WebApi.Helpers;
global using WebApi.Services;
global using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddJsonOptions(x => x
        .JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

// configure strong typed settings object
builder.Services.Configure<SpotifySettings>(builder.Configuration.GetSection("Spotify"));
builder.Services.Configure<ApplicationClientSettings>(builder.Configuration.GetSection("ApplicationClient"));



// configure DI fo application services
builder.Services.AddScoped<ISpotifyLoginService, SpotifyLoginService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// cors
app.UseCors(cors => cors
   .SetIsOriginAllowed(orgin => true)
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials());

// custom global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
