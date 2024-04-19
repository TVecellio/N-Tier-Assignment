using Domain.IItemRepository;
using Microsoft.EntityFrameworkCore;
using NTier.Data;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ItemsContext>(options =>
        options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ItemsContext>();
//database is connected


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddRazorPages(options =>
    {
        options.Conventions.AuthorizeFolder("/Items", "AdminPolicy");
    });


builder.Services.AddScoped<IItemRepository, ItemRepositoryEF>();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // check if we already have an admin role
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        // if not make the admin role
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // now we are going to make a default admin user
    var userManager =
        scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string email = "admin@mostuff.com";
    // DANGER! PASSWORD MUST BE:
    // 6+ chars
    // at least one non alphanumerc character
    // at least one digit ('0'-'9')
    // at least one uppercase ('A'-'Z')
    string password = "Password123!";

    // see if we have already created the user
    // if not create them and give them the admin role
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}


app.Run();
