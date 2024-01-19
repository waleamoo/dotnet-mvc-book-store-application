using Microsoft.EntityFrameworkCore;
using TechQwerty.BookStore.Data;
using TechQwerty.BookStore.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#if DEBUG
// 1. Add runtime compilation of razor file
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
#endif
// 2. Add Data Conrext 
builder.Services.AddDbContext<BookStoreContext>(
    options => options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=BookStore;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
    );
// 3. Add DI 
builder.Services.AddScoped<BookRepository, BookRepository>();
builder.Services.AddScoped<LanguageRepository, LanguageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles(); // allows use of static files in wwwwroot

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
