namespace KontofonMonitor.API.Extensions
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

        public static void AddSwagger(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.OperationFilter<SwaggerDefaultValues>();
                if (!builder.Environment.IsDevelopment())
                    options.DocumentFilter<BasePathDocumentFilter>();
                else
                    options.AddServer(CreateEnvironmentServerUrl());
            });
        }

        private static OpenApiServer CreateEnvironmentServerUrl()
        {
            var serverUrl = "https://ccace{environment}.erf01.net/kontofonmonitorapi";
            var serverVariables = new Dictionary<string, OpenApiServerVariable>
            {
                ["environment"] = new OpenApiServerVariable
                {
                    Default     = "stest",
                    Description = "Environment identifier.",
                    Enum        = new List<string> { "stest", "atest", "prod" }
                }
            };
            return new OpenApiServer
            {
                Url       = serverUrl,
                Variables = serverVariables
            };
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
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }
            });
        }
    }
}