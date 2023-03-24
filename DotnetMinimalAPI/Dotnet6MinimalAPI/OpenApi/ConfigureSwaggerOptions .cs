﻿namespace Dotnet6MinimalAPI.OpenApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var text = new StringBuilder("API Documentation for Contact Center kontofon Monitor API");
            var info = new OpenApiInfo()
            {
                Title = "Kontofon Monitor API",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact { Name = "Jitesh Sonkusare", Email = "jitesh.sonkusare@dnb.no" }
            };

            if (description.IsDeprecated)
            {
                text.Append("This API version has been deprecated.");
            }

            info.Description = text.ToString();
            return info;
        }
    }
}
