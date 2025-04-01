using MVCProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Registra el servicio TicketService y configura HttpClient
builder.Services.AddHttpClient<ITicketService, TicketService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7224/"); 
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ticket}/{action=Index}/{id?}");

app.Run();
