using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechQwerty.BookStore.Data;
using TechQwerty.BookStore.Helpers;
using TechQwerty.BookStore.Models;
using TechQwerty.BookStore.Repository;
using TechQwerty.BookStore.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#if DEBUG
// 1. Add runtime compilation of razor file
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
// 2. Add Data Context 
builder.Services.AddDbContext<BookStoreContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
// 5. Add Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = true;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = builder.Configuration["Application:LoginPath"];
});
// 3. Add DI 
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>(); // add the claims for the header 
builder.Services.AddScoped<IUserService, UserService>(); // get the user id in the controllers 
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.AddScoped<IEmailService, EmailService>(); // email service 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // allows use of static files in wwwwroot

app.UseRouting();

// 4. Add authentication - authentication comes before authorization 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
