namespace Dotnet6MinimalAPI.Endpoints.v2
{
    public static class ServiceApi
    {
        public static void MapServiceV2Endpoints(this WebApplication app, ApiVersionSet versionSet)
        {
            app.MapGet("v{version:apiVersion}/Services", GetServices)
                .AddMetaData<List<Models.Tjenester>>(
                    tag: "Service",
                    summary: "Get services with name and status",
                    description: "It will list all the services with name and their statuses from kontofon monitor application log file.")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(new ApiVersion(2));
        }

        static IResult GetServices(IService service)
        {
            var result = service.GetServices();

            if (result.Data == null)
                return Results.NotFound(new ErrorResponseModel { Message = result.Messages.Any() ? result.Messages.FirstOrDefault() : null, StatusCode = StatusCodes.Status404NotFound });

            return Results.Ok(result.Data);
        }
    }
}
