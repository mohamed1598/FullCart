using FullCart.API.Extensions;
using FullCart.API.Helpers;
using FullCart.API.Middlewares;
using FullCart.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentations();
builder.Services.ConfigureFileUpload();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentations();
}
await app.SeedUsers();
app.UseStaticFiles();
app.MapControllers();

app.Run();
