using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(o => o.AddDefaultPolicy(p => p.AllowAnyOrigin()));


builder.Services.AddSingleton<IAgent>(sp => new HttpAgent());

builder.Services.AddSingleton(sp => ActivatorUtilities.CreateInstance<GovernanceApiClient>(sp, Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai")));

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
