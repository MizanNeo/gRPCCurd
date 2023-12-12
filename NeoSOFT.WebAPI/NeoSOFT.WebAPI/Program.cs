using Asp.Versioning;
using AutoMapper;

using Microsoft.AspNetCore.Builder;
using NeoSOFT.Domain.Mapping;
using NeoSOFTWebAPI.Extentions;
using NeoSOFTWebAPI.Handlers;

var builder = WebApplication.CreateBuilder(args);


//TODO: Allow specific origin
builder.Services.ConfigureCors();
//DI for DB Connection
builder.Services.ConfigureDBConnection(builder.Configuration);
builder.Services.AddMemoryCache();
//Configure the HttpContext accessor
builder.Services.ConfigureHttpContext(builder.Configuration);

//DI for the Business services
builder.Services.ConfigureBusinessServices();


//Adding API versioning capabilities
builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
});

//Configure the Swagger with API Version & Securtiy requirement
builder.Services.ConfigureSwagger();
//DI for Repository
builder.Services.ConfigureRepositoryWrapper();


//Configure the AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("CorsPolicy");

app.UseMiddleware<RequestLoggingMiddleware>();

app.ConfigureExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
