using Microsoft.EntityFrameworkCore;
using TorqIQ.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
 

bool useSqlServer = builder.Configuration.GetValue<bool>("UseSqlServer");

if (useSqlServer)
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite") ?? "Data Source=maintainiq.db"));
}

var app = builder.Build();

 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    db.SeedIfEmpty();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
