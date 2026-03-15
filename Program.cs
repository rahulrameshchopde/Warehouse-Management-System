using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;

using System.Text;

using WarehouseProject.Data;
using WarehouseProject.Helpers;
using WarehouseProject.Services;
using WarehouseProject.Services.AuditLogs;
using WarehouseProject.Services.Identity_Access_Management;
using WarehouseProject.Services.Inventory_Stock_Control;
using WarehouseProject.Services.Picking_Packing_Dispatch;
using WarehouseProject.Services.Replenishment_Slotting;
using WarehouseProject.Services.Warehouse_Layout_Location_Management;



var builder = WebApplication.CreateBuilder(args);


// DATABASE CONNECTION

builder.Services.AddDbContext<WarehouseDBContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// DEPENDENCY INJECTION

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IWarehouseService, WarehouseService>();
        builder.Services.AddScoped<IZoneService, ZoneService>();
        builder.Services.AddScoped<IBinLocationService, BinLocationService>();
        builder.Services.AddScoped<IPutAwayService, PutAwayService>();
        builder.Services.AddScoped<IInventoryBalanceService, InventoryBalanceService>();
        builder.Services.AddScoped<IPickTaskService, PickTaskService>();
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<IInboundReceiptService, InboundReceiptService>();
        builder.Services.AddScoped<IStockReservationService, StockReservationService>();
        builder.Services.AddScoped<IPackingUnitService, PackingUnitService>();
        builder.Services.AddScoped<IShipmentService, ShipmentService>();
        builder.Services.AddScoped<IReplenishmentService, ReplenishmentService>();
        builder.Services.AddScoped<ISlottingRuleService, SlottingRuleService>();
        builder.Services.AddScoped<IWarehouseReportService, WarehouseReportService>();
        builder.Services.AddScoped<IDashboardService, DashboardService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IAuditLogService, AuditLogService>();
        builder.Services.AddScoped<AuditHelper>();




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


var app = builder.Build();


// MIDDLEWARE PIPELINE

app.UseHttpsRedirection();

app.UseAuthentication();   // IMPORTANT

app.UseAuthorization();

app.MapControllers();

app.Run();
