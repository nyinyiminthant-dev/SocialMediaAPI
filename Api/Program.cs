//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using BAL.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MODEL.DTOs;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings();


builder.Services.AddControllers();
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
ServiceManager.SetServiceInfo(builder.Services, appSettings);
builder.Services.AddScoped<CommonAuthentication>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddControllers();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.SaveToken = true;
       options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
       options.RequireHttpsMetadata = true;
       options.TokenValidationParameters = new()
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAL0zIKgOk+azCEuVZvrvtkgjRk3VcSq4 kDzbi51WD2xCUGNafzI8cmoY9KqFh7s1V7C6nw3/QbzvTytwYR/c5Q0CAwEAAQ==")),
           ValidateLifetime = true,
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidIssuer = "Allianz_DEV",
           ValidAudience = "Allianz_DEV",
           RoleClaimType = ClaimTypes.Role
       };
   });


var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("MyCorsPolicy");
app.MapControllers();

app.Run();

