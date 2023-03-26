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
app.MapEndpoints();
app.UseSwaggerInfo();
app.Run();