using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Concrete;
using ShopApp.Business.IoC;
using ShopApp.Business.Mappings;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.DataAccess.Identity;
using ShopApp.WebApi.Extentions;
using ShopApp.WebApi.SeedIdentity;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
       
    });

builder.Services.AddDbContext<ShopContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerShop")));
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerIdentity")));

builder.Services.AddIdentity<User, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    //options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Lockout.MaxFailedAccessAttempts = 20;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;



});
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfcoreGenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICartService, CartManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = true,
        Name = ".ShopApp.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CartMappingProfile>();
    cfg.AddProfile<OrderMappingProfile>();
    cfg.AddProfile<CategoryMappingProfile>();
    cfg.AddProfile<ProductMappingProfile>();
    cfg.AddProfile<UserMappingProfile>();
});

// Configure Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DependencyResolver());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy settings
string MyAllowOrigins = "_myAllowOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowOrigins, builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.ApplyPendingMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowOrigins);

app.UseAuthorization();

app.MapControllers();



await using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var cartService = services.GetRequiredService<ICartService>();
    var configuration = services.GetRequiredService<IConfiguration>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        await SeedIdentity.Seed(userManager, roleManager, cartService, configuration, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
