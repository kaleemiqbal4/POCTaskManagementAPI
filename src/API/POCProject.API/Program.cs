using POCProject.API;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureService();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigurePipeLine();
app.Run();