using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IUserStore, InMemoryUserStore>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { 

	options.Password.RequireDigit = true; 
	options.Password.RequiredLength = 6; 
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false; 
	options.Password.RequireLowercase = false;


options.User.RequireUniqueEmail = true;

}) .AddDefaultTokenProviders();


builder.Services.AddScoped<SignInManager>();

var app = builder.Build();


if (!app.Environment.IsDevelopment()) 
{
	app.UseExceptionHandler("/Home/Error"); 
	app.UseHsts();
}

app.UseHttpsRedirection(); 
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();