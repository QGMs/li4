using CyberForgeMS.Data;
using CyberForgeMS.Data.Utilizadores;
using CyberForgeMS.Data.Stock;
using CyberForgeMS.Data.Encomendas;
using CyberForgeMS.Business;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using CyberForgeMS.Business.Utilizadores;
using CyberForgeMS.Business.Encomendas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUtilizadoresDAO,UtilizadoresDAO>();
builder.Services.AddTransient<IComponenteDAO,ComponenteDAO>();
builder.Services.AddTransient<IStockDAO,StockDAO>();
builder.Services.AddTransient<IProdutosDAO,ProdutosDAO>();
builder.Services.AddTransient<IEncomendasDAO,EncomendasDAO>();
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<MontagemModel>();
builder.Services.AddSingleton<Carrinho>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // Required for Identity UI controllers

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
