using ApiWithAuthentication.Configurations;
using ApiWithAuthentication.Constants;
using ApiWithAuthentication.Data;
using ApiWithAuthentication.Services;
using ApiWithAuthentication.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ApiKeyConfiguration.ApiKey = builder.Configuration.GetValue<string>(ConfigurationConstants.ApiKey);

var jwtKey = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>(ConfigurationConstants.JwtKey));
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    // options => options.UseSqlite("DataSource=DbApp.db")
    options => options.UseInMemoryDatabase("342ba380ee3e412e84ad582fc8f93390")
    );

builder.Services.AddSingleton<JwtConfiguration>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<DataSeeding>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Order matters
app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

await app.Services.CreateScope().ServiceProvider.GetService<DataSeeding>()!.RunAsync();

app.Run();
