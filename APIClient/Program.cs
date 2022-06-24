using APIClient.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


#region Servicios

var CorsPolicy = "_corsPolicy";

//Para que el navegador no bloquee los metodos post put 
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: CorsPolicy,
//      builder =>
//      {
//          builder.WithOrigins("http://localhost:7173",
//                              "https://localhost:7173",

//                              "http://192.168.120.44",
//                              "https://192.168.120.44",

//                              "http://192.168.30.10",
//                              "https://192.168.30.10",

//                              "http://localhost",
//                              "https://localhost",

//                              "http://localhost:5001",
//                              "https://localhost:5001")
//           .AllowAnyMethod()
//           .AllowAnyHeader()
//           .AllowCredentials();
//      });
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
      builder =>
      {
          builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed(origin => true); // allow any origin;
      });
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers();


builder.Services.AddDataProtection();

//Injection user
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEncrypterService, EncrypterService>();

var llave = Encoding.ASCII.GetBytes("fda4c1bf82f274bd752c38b601f3a8cd727b8a6857eae747302c0867dea17bc9");

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


builder.Services.AddHttpsRedirection(opts =>
{
    opts.HttpsPort = 5001;
});


#region Signal
builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(opts =>
        opts.PayloadSerializerSettings.TypeNameHandling = TypeNameHandling.Auto); ;

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
#endregion

IWebHostEnvironment env = builder.Environment;

#endregion



#region APP
var app = builder.Build();

app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHttpsRedirection();
}

//#region AÑADIR PARA VER LAS IMAGENES
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"data")),
//    RequestPath = new PathString("/data")
//});
//#endregion AÑADIR PARA VER LAS IMAGENES


app.UseRouting();

app.UseCors(CorsPolicy);


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapHub<DataService>("/" + URLs.ReportSignal);
app.UseEndpoints(endpoints =>
{
    app.MapHub<SignalRService>("signalr/disparohub");
});

app.Run();
#endregion