var builder = WebApplication.CreateBuilder(args);

builder.AddDIDependencies();
builder.AddNLog();
builder.Services.AddEndpointsApiExplorer();
builder.AddSwagger();
builder.AddApiVersioningInfo();

var app = builder.Build();

app.UseHttpsRedirection();
var versionSet = app.GetApiVersionSet();
app.MapServiceEndpoints(versionSet);
app.UseSwaggerInfo();

app.Run();
