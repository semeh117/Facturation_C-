using FacturationTn.Web.Components;
using FacturationTn.Infrastructure.Persistence;
using FacturationTn.Application.Services;
using Microsoft.EntityFrameworkCore;
using Radzen;
// 1. ADD THESE NAMESPACES
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register the Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. CONFIGURING IDENTITY AS PER TEACHER'S SPECS
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddCascadingAuthenticationState();

// Register the Application Services
builder.Services.AddScoped<FactureService>();
builder.Services.AddRadzenComponents();

var app = builder.Build();

// 3. SEEDING THE DEFAULT ADMIN ACCOUNT
// 3. SEEDING THE DEFAULT ADMIN ACCOUNT
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Keep the role as "Admin" so your @attribute [Authorize(Roles = "Admin")] directives work perfectly
    if (!await roleManager.RoleExistsAsync("Admin"))
        await roleManager.CreateAsync(new IdentityRole("Admin"));

    var existingUser = await userManager.FindByEmailAsync("semeh@gmail.com");
    if (existingUser == null)
    {
        var adminUser = new IdentityUser { UserName = "semeh@gmail.com", Email = "semeh@gmail.com" };
        var result = await userManager.CreateAsync(adminUser, "semeh123");

        if (result.Succeeded)
            await userManager.AddToRoleAsync(adminUser, "Admin");
    }
    else
    {
        // Ensure UserName is correct if user already exists
        if (existingUser.UserName != "semeh@gmail.com")
        {
            existingUser.UserName = "semeh@gmail.com";
            await userManager.UpdateAsync(existingUser);
        }
        
        // Ensure user is in the Admin role
        if (!await userManager.IsInRoleAsync(existingUser, "Admin"))
        {
            await userManager.AddToRoleAsync(existingUser, "Admin");
        }

        // FORCE PASSWORD RESET to ensure it matches teacher's requirements
        var token = await userManager.GeneratePasswordResetTokenAsync(existingUser);
        await userManager.ResetPasswordAsync(existingUser, token, "semeh123");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

// 4. ENABLE CHECKPOINT GATES
app.UseAuthentication(); 
app.UseAuthorization();  

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// =========================================================================
// 5. TEACHER'S APPROACH: MINIMAL API AUTHENTICATION ENDPOINTS (Outside WebSocket)
// =========================================================================
app.MapPost("/api/auth/login", async (
    [FromServices] SignInManager<IdentityUser> signInManager,
    [FromForm] string email, 
    [FromForm] string password) =>
{
    // This traditional HTTP call is allowed to issue browser cookies safely!
    var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
    
    if (result.Succeeded) return Results.Redirect("/"); // Redirects to dashboard on success
    
    return Results.Redirect("/login?error=Identifiants+incorrects");
}).DisableAntiforgery(); 

app.MapPost("/api/auth/logout", async ([FromServices] SignInManager<IdentityUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
}).DisableAntiforgery();
// =========================================================================

app.Run();