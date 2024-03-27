using BE;
using DAO.DepositRequest;
using DAO.Orchid;
using DAO.Transaction;
using DAO.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.DepositRequest;
using Repositories.Transaction;
using Repositories.User;
using Services.Auth;
using Services.Blockchain;
using Services.Common.Firebase;
using Services.Common.Gprc.Nft;
using Services.Common.Jwt;
using Services.Common.Sha256;
using Services.DepositRequest;
using Services.Orchid;
using Services.Transaction;
using Services.User;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// add automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// add scoped
builder.Services.AddControllers().AddJsonOptions(options =>
 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddTransient<IUserDAO, UserDAO>();
builder.Services.AddTransient<IOrchidDAO, OrchidDAO>();
builder.Services.AddTransient<IDepositRequestDAO, DepositRequestDAO>();
builder.Services.AddTransient<ITransactionDAO, TransactionDAO>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOrchidRepository, OrchidRepository>();
builder.Services.AddTransient<IDepositRequestRepository, DepositRequestRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<ISha256Service, Sha256Service>();
builder.Services.AddSingleton<IFirebaseService, FirebaseService>();

builder.Services.AddSingleton<INftGrpcService, NftGrpcService>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IOrchidService, OrchidService>();
builder.Services.AddSingleton<IBlockchainService, BlockchainService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDepositRequestService, DepositRequestService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();

builder.Services.AddEndpointsApiExplorer();

//config swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateActor = false,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
