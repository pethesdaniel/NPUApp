using NPUApp.BLL.Mapping;
using NPUApp.BLL.Services;
using NPUApp.BLL.Validation;
using NPUApp.Database.Context;
using NPUApp.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//stubbed initial data
builder.Services.AddDbContext<NpuAppDbContext>();
builder.Services.SeedDbWithData();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<PostsService>();
builder.Services.AddScoped<RatingsService>();
builder.Services.AddScoped<PartsService>();

builder.Services.AddBllValidators();

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

app.Run();
