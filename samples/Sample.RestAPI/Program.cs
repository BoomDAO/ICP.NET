using ICP.Agent.Agents;
using ICP.Agent.Auth;
using Sample.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin()));

Uri url = new Uri($"https://ic0.app");
var identity = new AnonymousIdentity();
builder.Services.AddSingleton<IAgent>(sp => new HttpAgent(identity, url));

builder.Services.AddSingleton<GovernanceApiClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();
