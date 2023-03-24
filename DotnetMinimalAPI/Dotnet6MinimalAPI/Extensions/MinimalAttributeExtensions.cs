namespace Dotnet6MinimalAPI.Extensions
{
    public static class MinimalAttributeExtensions
    {
        public static RouteHandlerBuilder AddMetaData<T>(this RouteHandlerBuilder endpoint, string tag, string? summary = null, string? description = null)
        {
            endpoint.WithTags(tag);

            endpoint.WithMetadata(new SwaggerOperationAttribute(summary, description));

            endpoint.WithMetadata(new SwaggerResponseAttribute(200, type: typeof(T)))
                    .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseModel)))
                    .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseModel)));

            return endpoint;
        }

        public static RouteHandlerBuilder AddMetaData(this RouteHandlerBuilder endpoint, string tag, string? summary = null, string? description = null)
        {
            endpoint.WithTags(tag);

            endpoint.WithMetadata(new SwaggerOperationAttribute(summary, description));

            endpoint.WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseModel)))
                    .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseModel)));

            return endpoint;
        }
    }
}
