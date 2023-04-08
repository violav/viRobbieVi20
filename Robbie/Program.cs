using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Robbie.Services;
using Services;
using Services.Models.Contracts;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<MongoDBSettings>(builder.Configuration.GetSection(MongoDBSettings.MongoDB))
    .Configure<EmailLibrarySettings>(builder.Configuration.GetSection(EmailLibrarySettings.EmailLibrary));

builder.Services
    .AddSingleton<SupplierService>()
    .AddSingleton<CondominiumPeopleService>()
    .AddSingleton<GreenPrunningService>()
    .AddSingleton<EmailService>()
    .AddSingleton<GroundService>()
    .AddSingleton<FireService>()
    .AddSingleton<LiftService>()
    .AddSingleton<ImportsService>()
    .AddSingleton<UserService>()
    .AddSingleton<CondominiumService>()
    .AddSingleton<AccidentService>()
    .AddSingleton<SearchService>()
    .AddSingleton<AssembleeService>()
    .AddControllers()
    .AddJsonOptions(jsonOpt =>
    {
        jsonOpt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        c.UseInlineDefinitionsForEnums();
    });


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app
    .UseHttpsRedirection()
    .UseAuthorization();

app.MapControllers();
app.Run();
