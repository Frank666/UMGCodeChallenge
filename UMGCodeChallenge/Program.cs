using UMGCodeChallenge.Application.endpoints;
using UMGCodeChallenge.Domain;
using UMGCodeChallenge.endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(7088, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IContractServiceFactory, ContractServiceFactory>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var controller = new ProcessFile(app.Services.GetRequiredService<IContractServiceFactory>());


app.MapPost("/contracts/upload", controller.UploadContracts)
    .Accepts<ContractUploadRequest>("multipart/form-data")
    .Produces<IEnumerable<MusicContract>>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .DisableAntiforgery()
    .WithOpenApi();

app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.Run();