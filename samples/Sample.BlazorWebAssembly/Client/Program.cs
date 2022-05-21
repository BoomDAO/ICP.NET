using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sample.BlazorWebAssembly.Client;
using Sample.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7288") });

Uri url = new Uri($"https://ic0.app");

var identity = new AnonymousIdentity();
builder.Services.AddSingleton<IAgent>(sp => new HttpAgent(identity, url));

builder.Services.AddSingleton<GovernanceApiClient>();

await builder.Build().RunAsync();
