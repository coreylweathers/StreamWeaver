using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Auth0 OIDC Authentication setup
builder.Services.AddOidcAuthentication(options =>
{
    //builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.Authority = "https://dev-xe3ykqq7.us.auth0.com";
    options.ProviderOptions.ClientId = "u0FDFJf916cYRab32rOrNSDeYMAdJHHE";
    options.ProviderOptions.ResponseType = "code";
    // Uncomment and set the Audience if you need an access token for an API
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    // options.ProviderOptions.AdditionalProviderParameters.Add("audience", "YOUR_AUTH0_API_AUDIENCE");

    // Configure the post-login redirect URI
    // Make sure these are absolute URLs
    var baseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    options.ProviderOptions.RedirectUri = $"{baseAddress}authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = $"{baseAddress}";
    
    // Add this to fix the logout issue
    options.ProviderOptions.AdditionalProviderParameters.Add("prompt", "login");
});

await builder.Build().RunAsync();