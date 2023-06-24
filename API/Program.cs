using API.Contracts;
using API.Repositories;
using API.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProjectManagementDBContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
/*builder.Services.AddScoped<IFileRepository, FileRepository>();
*/
builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        //policy.WithOrigins("http://localhost:3000", "https://localhost:3223");
        policy.AllowAnyHeader();
        //policy.WithHeaders("content-type", "authorization");
        policy.AllowAnyMethod();
        //policy.WithMethods("GET", "POST", "PUT", "DELETE");
    });

    /*options.AddPolicy("Tokopedia", policy => {
        policy.WithOrigins("http://www.tokopedia.co.id");
        policy.AllowAnyHeader();
        policy.WithMethods("GET", "POST");
    });
    
    options.AddPolicy("GoPay", policy => {
        policy.WithOrigins("http://www.tokopedia.co.id");
        policy.AllowAnyHeader();
        policy.WithMethods("PUT", "POST");
    });*/
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => {
           options.RequireHttpsMetadata = false;
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false,
               ValidAudience = builder.Configuration["JWT:Audience"],
               ValidateIssuer = false,
               ValidIssuer = builder.Configuration["JWT:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
       });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
