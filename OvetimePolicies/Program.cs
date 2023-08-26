using Microsoft.EntityFrameworkCore;
using OvetimePolicies_Core.Dtos;
using OvetimePolicies_Data;
using OvetimePolicies_Data.Handlers;
using OvetimePolicies_Data.Repositories;
using OvetimePolicies_dlls;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new CustomSlashInputFormatter<AddCommandDto>());
    options.InputFormatters.Insert(0, new CustomSlashInputFormatter<EditPersonDto>());
})
.AddXmlDataContractSerializerFormatters()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("myAppcs")));

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IQueryRepository, QueryRepository>();

builder.Services.AddScoped<CalculatorHandler>();
builder.Services.AddScoped<AddSalaryHandler>();
builder.Services.AddScoped<EditSalaryHandler>();
builder.Services.AddScoped<DeleteSalaryHandler>();

builder.Services.AddScoped<GetByIdHandler>();
builder.Services.AddScoped<GetAllHandler>();

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
