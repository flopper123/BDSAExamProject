using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

using LitExplore.Core.Publication;
using LitExplore.Core.Filter;

using LitExplore.Entity;
using LitExplore.Entity.Repositories;
using LitExplore.Entity.Context;

using LitExplore.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<GraphController>();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services
    .AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options => {});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();


var factory = new LitExploreContextFactory();
var context = factory.CreateDbContext(new string[] {});

builder.Services.AddDbContext<LitExploreContext>(options => LitExploreContextFactory.GetOptions());

builder.Services.AddTransient<IFilterRepository<PublicationGraph>, FilterRepository<PublicationGraph>>(
    provider => {
        var ctx = new LitExploreContextFactory().CreateDbContext(new string[] {});
        Seed.SeedDB(ctx);
        return new FilterRepository<PublicationGraph>(ctx);
    }
);
builder.Services.AddTransient<IPublicationRepository, PublicationRepository>(
    provider => {
        var ctx = new LitExploreContextFactory().CreateDbContext(new string[] {});
        Seed.SeedDB(ctx);
        return new PublicationRepository(ctx);
    }
);


// If database has not been seeded, seed it



builder.Services.AddSingleton<GraphController>();


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

app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
  endpoints.MapBlazorHub();
  endpoints.MapFallbackToPage("/_Host");
});

app.Run();
