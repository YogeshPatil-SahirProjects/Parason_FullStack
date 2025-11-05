using Microsoft.EntityFrameworkCore;
using Parason_Api.Models;
using Parason_Api.Services;
using Parason_Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database context
builder.Services.AddDbContext<ParasonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IAttributeDefService, AttributeDefService>();
builder.Services.AddScoped<IAttributeListValueService, AttributeListValueService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IItemMasterService, ItemMasterService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddScoped<IQuoteHeaderService, QuoteHeaderService>();
builder.Services.AddScoped<IQuoteEquipmentOrModelService, QuoteEquipmentOrModelService>();
builder.Services.AddScoped<IQuoteVerticalService, QuoteVerticalService>();
builder.Services.AddScoped<IScopeOfSupplyService, ScopeOfSupplyService>();
builder.Services.AddScoped<ISeriesService, SeriesService>();
builder.Services.AddScoped<ISpecDetailService, SpecDetailService>();
builder.Services.AddScoped<IVerticalAreaService, VerticalAreaService>();


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