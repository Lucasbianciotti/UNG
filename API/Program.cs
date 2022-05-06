using API.LocalModels;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

#region Servicios


//Para que el navegador no bloquee los metodos post put 
builder.Services.AddCors(options =>
{
    //options.AddPolicy("CorsPolicy",
    //    builder => builder
    //        .AllowAnyMethod()
    //        .AllowCredentials()
    //        .AllowAnyHeader()
    //        .SetIsOriginAllowed((host) => true));

    options.AddPolicy(name: "CorsPolicy",
      builder =>
      {
          builder.WithOrigins("https://localhost:7107", "https://localhost:7108")
                                .AllowAnyMethod()
                                .AllowCredentials()
                                .AllowAnyHeader();
      });
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();


builder.Services.AddDataProtection();

//Injection user
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEncrypterService, EncrypterService>();

//Para cargar el objeto AppSettings con lo que esta en appsettings.json
var appSettingsSection = builder.Configuration.GetSection("AppSetings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var llave = Encoding.ASCII.GetBytes("5747511683d38b9e4e53070df9b16c1bcc35796fd89c3bab0504d29a89de1e8e");

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);

    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(opts =>
        opts.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Auto); ;

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
#endregion




#region APP
var app = builder.Build();

app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();



app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapHub<DataService>("/" + URLs.ReportSignal);
app.MapHub<DataService>("/disparohub");

app.Run();
#endregion