using Asp.Versioning;
using AutoMapper;
using NeoSOFT.GRPCServices.Extentions;
using NeoSOFT.GRPCServices.Services;
using Microsoft.AspNetCore.Builder;
using NeoSOFT.Domain.Mapping;
using NeoSOFT.GRPCServices.Handlers;


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

// Add services to the container.
builder.Services.AddGrpc();

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
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<departMasterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
