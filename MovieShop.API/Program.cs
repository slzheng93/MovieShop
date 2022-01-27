using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Servicces;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieShop.API.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ICastService, CastService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICurrentLoginUserService, CurrentLoginUserService>();


builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IRepository<Genre>, EfRepository<Genre>>();
builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();


// inject HttpContext
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject the connection string
builder.Services.AddDbContext<MovieShopDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
    }
    );

// Specify JWt Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["PrivateKey"]))
        };
    });

var app = builder.Build();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.WithOrigins(app.Configuration.GetValue<string>("clientSPAUrl")).AllowAnyHeader()
        .AllowAnyMethod().AllowCredentials();
});




// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();


// add authentication Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
