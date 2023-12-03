using System.Text;
using DoctorTrainer.Data;
using DoctorTrainer.Repository;
using DoctorTrainer.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();

// repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserTokenRepository>();
builder.Services.AddScoped<ImageDataRepository>();

// services
builder.Services.AddScoped<ImageDataService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<ImageAnalysisService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7171/",
            ValidAudience = "https://localhost:7171/",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["SigningKey"])
            )
        };
    });

builder.Services.AddMvc();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
    .AllowCredentials()); // allow credentials

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
