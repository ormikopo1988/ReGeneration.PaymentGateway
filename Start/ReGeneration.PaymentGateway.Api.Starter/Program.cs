using ReGeneration.PaymentGateway.Api.Starter.Application;
using ReGeneration.PaymentGateway.Api.Starter.Filters;
using ReGeneration.PaymentGateway.Api.Starter.Persistence;
using ReGeneration.PaymentGateway.Api.Starter.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await Helpers.MigrateDbIfNeededAndSeedItWithSampleDataAsync(app);

app.Run();
