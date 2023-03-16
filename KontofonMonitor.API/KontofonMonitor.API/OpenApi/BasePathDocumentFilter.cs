namespace KontofonMonitor.API.OpenApi
{
    public class BasePathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>
            {
                new OpenApiServer() { Description="DEV", Url=$"https://localhost:7294" },
            };
        }
    }
}
