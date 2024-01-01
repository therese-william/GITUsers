using GITUsers.API.Infrastructure;
using GITUsers.API.Services;
using Microsoft.Net.Http.Headers;
using Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

ConfigureLogging(builder);

// Add services to the container.
ConfigureServices(builder, config);

var app = builder.Build();

ConfigureApp(app);

app.Run();

static void ConfigureServices(WebApplicationBuilder builder, ConfigurationManager config)
{
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddMemoryCache();

    builder.Services.AddHttpClient<IUserClient, UserClient>(httpClient =>
    {
        httpClient.BaseAddress = new Uri(config["GitHubUsersUrl"]);
        httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
    })
    .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(3000)))
    .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));

    builder.Services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
    builder.Services.AddSingleton<IUsersService, UsersService>();
}

static void ConfigureApp(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

static void ConfigureLogging(WebApplicationBuilder builder)
{
    var logger = new LoggerConfiguration()
      .ReadFrom.Configuration(builder.Configuration)
      .Enrich.FromLogContext()
      .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
}