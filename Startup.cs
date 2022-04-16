using System.Text;
using System.Text.Json;
using GraduationProject.Data;
using GraduationProject.Utilities;
using GraduationProject.Utilities.AuthenticationConfigurations;
using GraduationProject.Utilities.CustomApiResponses;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;

namespace GraduationProject
{
	public class Startup
	{
		private readonly IConfiguration _config;

		public Startup(IConfiguration config)
		{
			_config = config;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<ApplicationDbContext>(options =>
				options.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

			services.AddAuthentication(options =>
			{
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateLifetime = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _config["JWT:Issuer"],
					ValidAudience = _config["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]))
				};
			})
			.AddCookie(options => options.LoginPath = "/Account/SignIn");

			services.AddControllersWithViews()
				.AddRazorRuntimeCompilation()
				.AddNToastNotifyToastr(new ToastrOptions()
				{
					ProgressBar = true,
					PositionClass = ToastPositions.TopRight,
					PreventDuplicates = true,
					CloseButton = true
				})
				.ConfigureApiBehaviorOptions(options =>
					options.InvalidModelStateResponseFactory = actionContext => new BadRequest(actionContext.ModelState))
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
					options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
					options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				});

			services.AddAutoMapper(options => options.AddProfile<MapperProfile>());
			services.AddScoped<IAuthenticationTokenGenerator, JwtGenerator>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseDeveloperExceptionPage();
			app.UseStaticFiles();
			app.Use(async (context, next) =>
			{
				if (context.Request.Method == "POST")
					HttpRequestLogger.Log(context.Request);

				await next.Invoke();
			});

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
		}
	}
}