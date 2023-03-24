namespace Dotnet6MinimalAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDIDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IService, Service>();
            builder.Services.Configure<KontofonMonitorConfig>(builder.Configuration.GetSection(nameof(KontofonMonitorConfig)));
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        public static void AddNLog(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();
        }

        public static void AddApiVersioningInfo(this WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static void AddCorsInfo(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
                options.AddServer(CreateEnvironmentServerUrl(builder));
            });
        }

        public static ApiVersionSet GetApiVersionSet(this WebApplication app)
        {
            return app.NewApiVersionSet()
                        .HasApiVersion(1, 0)
                        .HasApiVersion(2, 0)
                        .ReportApiVersions()
                        .Build();
        }

        public static void UseSwaggerInfo(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();
                foreach (var description in descriptions)
                {
                    var url = $"{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }

        private static OpenApiServer CreateEnvironmentServerUrl(WebApplicationBuilder builder)
        {
            // Instead of this you can add names like stest, atest, prod
            var enums = builder.Environment.IsDevelopment() ?
                new List<string>() { "7253", "5170" } :
                new List<string> { "18089"};

            var serverVariables = new Dictionary<string, OpenApiServerVariable>
            {
                ["Environment"] = new OpenApiServerVariable
                {
                    Default = enums[0],
                    Description = "Environment identifier.",
                    Enum = enums
                }
            };
            return new OpenApiServer
            {
                Url = "http://localhost:{Environment}",
                Variables = serverVariables
            };
        }
    }
}