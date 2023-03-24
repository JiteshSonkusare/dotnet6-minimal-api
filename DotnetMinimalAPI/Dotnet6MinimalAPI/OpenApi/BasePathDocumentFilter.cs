namespace Dotnet6MinimalAPI.OpenApi
{
    public class BasePathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>
            {
                new OpenApiServer() { Description="DEV", Url=$"http://localhost:44397/KontofonMonitorApi" },
                new OpenApiServer() { Description="DEV", Url=$"http://kontofonmonitorapi-dev.azurewebsites.net" },
                new OpenApiServer() { Description = "DST" , Url = "http://drfwdapp21.drf01.net/kontofonmonitorapi" },
            };
        }
    }
}