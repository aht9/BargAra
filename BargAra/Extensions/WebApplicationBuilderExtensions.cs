namespace BargAra.Extensions;

public static class WebApplicationBuilderExtensions
{

    public static WebApplicationBuilder AddAppSettingBuilder(this WebApplicationBuilder builder)
    {
        // Load appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ConfigurationFiles",
            builder.Environment.EnvironmentName);
        ConstClass.SetCurrentEnvironment(builder.Environment.EnvironmentName);
        builder.Configuration.AddJsonFile(Path.Combine(configFolderPath, "appsettings.Logging.json"), optional: true,
            reloadOnChange: true);
        builder.Configuration.AddJsonFile(Path.Combine(configFolderPath, "appsettings.ConnectionStrings.json"),
            optional: true, reloadOnChange: true);
        return builder;
    }
    
    public static WebApplicationBuilder AddDBContextBuilder(this WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        // Configure DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(dbConnectionString, optionsBuilder =>
            {
                optionsBuilder.EnableRetryOnFailure(3);
                optionsBuilder.CommandTimeout(3000);
            });
            options.EnableSensitiveDataLogging(false);
            options.EnableDetailedErrors(false);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Transient);

        return builder;
    }

    public static WebApplicationBuilder AddServicesBuilder(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddMemoryCache();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return builder;
    }
    
    public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");cd
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(dbConnectionString,
                        sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(dbConnectionString,
                        sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            })
            .AddAspNetIdentity<User>();
        
        builder.Services.AddControllersWithViews();
        
        return builder;
    }

}