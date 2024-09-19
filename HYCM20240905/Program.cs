using HYCM20240905AutenticacionJWTMinimalApi.Auth;
using HYCM20240905.Properties.Auth;
using HYCM20240905.Properties.Endpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT API", Version = "v1" });

    var jwtsecuritySchema = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingresar tu token de jwt Authetication",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtsecuritySchema.Reference.Id, jwtsecuritySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtsecuritySchema, Array.Empty<string>() } });
});

// Add authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LoggeInPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

var key = "key_JWTAPIMinimal2023.API";

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false
        };
    });

builder.Services.AddSingleton<IJwtAuthenticationService>(new JwtAuthenticationService(key));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddAccountEndpoint();
app.AddcategoriaEndpoints();
app.AddBodegaEndpoints();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
