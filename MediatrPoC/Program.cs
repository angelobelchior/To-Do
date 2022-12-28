using MediatrPoC.Presentation;

var assembly = typeof(Program).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation(assembly);
builder.Services.AddApplication(assembly);
builder.Services.AddInfrastructure();

var app = builder.Build();
app.UsePresentation();
app.Run();