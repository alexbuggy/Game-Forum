using GameForum.Data;
using GameForum.Models;
using GameForum.Repositories;
using GameForum.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameForum.Services;
using GameForum.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDiscussionRepository, DiscussionRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameRequestRepository, GameRequestRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IReplyRepository, ReplyRepository>();
builder.Services.AddScoped<IFavoriteGameRepository, FavoriteGameRepository>();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDiscussionService, DiscussionService>();
builder.Services.AddScoped<IReplyService, ReplyService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IFavoriteGameService, FavoriteGameService>();
builder.Services.AddScoped<IGameRequestService, GameRequestService>();

builder.Services.AddDbContext<GameForumContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<GameForumContext>()
.AddDefaultTokenProviders();

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IEmailSender, NullEmailSender>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); // Needed for Identity UI to work

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Create Admin role if it doesn't exist
    var roleExists = await roleManager.RoleExistsAsync("Admin");
    if (!roleExists)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Assign role to a specific user (by email)
    var adminEmail = "marcelo@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser != null && !(await userManager.IsInRoleAsync(adminUser, "Admin")))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();
