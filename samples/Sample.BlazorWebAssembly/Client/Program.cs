using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sample.BlazorWebAssembly.Client;
using Sample.Shared.Governance;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7288") });

builder.Services.AddSingleton<IAgent>(sp => new HttpAgent());
builder.Services.AddSingleton(sp => ActivatorUtilities.CreateInstance<GovernanceApiClient>(sp, Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai")));

await builder.Build().RunAsync();
