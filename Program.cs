using WebApi.Helpers;
using WebApi.Services;
using System.Text.Json.Serialization;
using SpotifyAPI.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// configure strong typed settings object
builder.Services.Configure<SpotifySettings>(builder.Configuration.GetSection("Spotify"));
builder.Services.Configure<ApplicationWebSettings>(builder.Configuration.GetSection("ApplicationWeb"));
builder.Services.Configure<ApplicationAPISettings>(builder.Configuration.GetSection("ApplicationAPI"));
builder.Services.Configure<CookiePolicyOptions>(options => {
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.OnAppendCookie = cookieContext => {
        cookieContext.CookieOptions.SameSite = SameSiteMode.None; // required due to not the same protocol (http vs https)
        cookieContext.CookieOptions.Secure = true; // required due to not the same protocol (http vs https)
        cookieContext.CookieOptions.HttpOnly = true;
    };
    options.OnDeleteCookie = cookieContext => {
        cookieContext.CookieOptions.SameSite = SameSiteMode.None; // required due to not the same protocol (http vs https)
        cookieContext.CookieOptions.Secure = true; // required due to not the same protocol (http vs https)
        cookieContext.CookieOptions.HttpOnly = true;
    };
});

// configure routing option
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

// configure DI fo application services
builder.Services.AddSingleton(SpotifyClientConfig.CreateDefault());
builder.Services.AddTransient<ErrorHandlerMiddleware>();
builder.Services.AddTransient<ISpotifyAuthService, SpotifyAuthService>();
builder.Services.AddTransient<ISpotifyService, SpotifyService>();

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

app.UseCookiePolicy();
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
