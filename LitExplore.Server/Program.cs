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

builder.Services.AddAuthorization(options =>
{
              // By default, all incoming requests will be authorized according to the default policy
              options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();


builder.Services.AddDbContext<LitExploreContext>(options => LitExploreContextFactory.GetOptions());

var context = new LitExploreContextFactory().CreateDbContext(new string[] {});
builder.Services.AddTransient<IFilterRepository<PublicationGraph>, FilterRepository<PublicationGraph>>(
    provider => {
        return new FilterRepository<PublicationGraph>(context);
    }
);
builder.Services.AddTransient<IPublicationRepository, PublicationRepository>(
    provider => {
        Seed.SeedDB(context);
        return new PublicationRepository(context);
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
