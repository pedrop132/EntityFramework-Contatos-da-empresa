using WebApiForEmpresa.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner
builder.Services.AddControllersWithViews();

// Comunication with the API || Portuguese comments are made in by default.
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<ContatoController>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44388/api/");
});

var app = builder.Build();

// Configure o pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padr�o do HSTS � 30 dias. Voc� pode querer mudar isso para cen�rios de produ��o, veja https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
