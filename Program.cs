using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WarehousePro.API.Services;
using WarehousePro.API.Services.Interfaces;
using WarehouseProject.Data;

using WarehouseProject.Services;


using WarehouseProject.Services.Order;

using WarehouseProject.Services.Replenishment_Slotting;





var builder = WebApplication.CreateBuilder(args);

// DATABASE CONNECTION

builder.Services.AddDbContext<WarehouseDBContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//for Session----Remembering the loggedin username

//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options => {
//    options.IdleTimeout = TimeSpan.FromSeconds(20);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});


// 🔹 CORS (IMPORTANT)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});


// DEPENDENCY INJECTION

builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IWarehouseService, WarehouseService>();
        builder.Services.AddScoped<IZoneService, ZoneService>();
        builder.Services.AddScoped<IBinLocationService, BinLocationService>();
        builder.Services.AddScoped<IPutAwayTaskService, PutAwayTaskService>();
        builder.Services.AddScoped<IInventoryBalanceService, InventoryBalanceService>();
        builder.Services.AddScoped<IPickTaskService, PickTaskService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        builder.Services.AddScoped<IInboundReceiptService, InboundReceiptService>();
        builder.Services.AddScoped<IStockReservationService, StockReservationService>();
        builder.Services.AddScoped<IPackingUnitService, PackingService>();
        builder.Services.AddScoped<IShipmentService, ShipmentService>();
        builder.Services.AddScoped<IReplenishmentService, ReplenishmentService>();
        builder.Services.AddScoped<ISlottingRuleService, SlottingRuleService>();
        builder.Services.AddScoped<IWarehouseReportService, WarehouseReportService>();
        builder.Services.AddScoped<IDashboardService, DashboardService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IAuditLogService, AuditLogService>();
       
builder.Services.AddControllers()

.AddJsonOptions(opt =>

{

opt.JsonSerializerOptions.Converters

    .Add(new JsonStringEnumConverter());

});




// JWT AUTHENTICATION

//var jwt = builder.Configuration.GetSection("Jwt");


builder.Services.AddAuthentication(options =>

{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})

        .AddJwtBearer(options =>

        {

            options.TokenValidationParameters = new TokenValidationParameters

            {

                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                //ValidIssuer = jwt["Jwt:Issuer"],

                //ValidAudience = jwt["Jwt:Audience"],

                //IssuerSigningKey = new SymmetricSecurityKey(

                //    Encoding.UTF8.GetBytes(["Jwt:Key"])
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(

                   Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                )

            };

        });


// ROLE AUTHORIZATION

builder.Services.AddAuthorization(options =>

{


    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    options.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Manager", "Admin"));

});



builder.Services.AddControllers()

    .AddJsonOptions(x =>

        x.JsonSerializerOptions.ReferenceHandler =

        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);




builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); 

var app = builder.Build();
//app.UseSession();



// MIDDLEWARE PIPELINE
app.UseCors("AllowAngular");


// app.UseHttpsRedirection();

app.UseAuthentication();   // IMPORTANT

app.UseAuthorization();

app.MapControllers();

app.Run();
