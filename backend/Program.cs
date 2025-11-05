using Microsoft.EntityFrameworkCore;
using Parason_Api.DTOs;
using Parason_Api.Models;
using Parason_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database context
builder.Services.AddDbContext<CPQDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IVerticalAreaService, VerticalAreaService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<ISeriesService, SeriesService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IAttributeDefService, AttributeDefService>();
builder.Services.AddScoped<IAttributeListValueService, AttributeListValueService>();
builder.Services.AddScoped<IItemMasterService, ItemMasterService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IVerticalProcessService, VerticalProcessService>();
builder.Services.AddScoped<IProcessEquipmentService, ProcessEquipmentService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IQuoteVerticalService, QuoteVerticalService>();
builder.Services.AddScoped<IQuoteEquipmentOrModelService, QuoteEquipmentOrModelService>();
builder.Services.AddScoped<IScopeOfSupplyService, ScopeOfSupplyService>();
builder.Services.AddScoped<ISpecDetailsService, SpecDetailsService>();
builder.Services.AddScoped<ICatalogService, CatalogService>(); 

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();