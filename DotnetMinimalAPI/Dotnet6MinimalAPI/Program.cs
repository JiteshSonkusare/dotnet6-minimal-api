using Dotnet6MinimalAPI.Endpoints.v1;
using Dotnet6MinimalAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddDIDependencies();
builder.AddNLog();
builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();
builder.AddApiVersioningInfo();
builder.AddCorsInfo();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors();
var versionSet = app.GetApiVersionSet();
app.MapServiceEndpoints(versionSet);
app.UseSwaggerInfo();

app.Run();