using Microsoft.EntityFrameworkCore;
using AriaRPG.Data;
using AriaRPG.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
    });

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "data", "aria.db");
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

builder.Services.AddDbContextFactory<AriaDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddScoped<AriaService>();

var app = builder.Build();

// Create/migrate database
using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AriaDbContext>>();
    using var db = factory.CreateDbContext();
    db.Database.EnsureCreated();

    // Auto-add SessionNotes table for existing databases
    try
    {
        db.Database.ExecuteSqlRaw(@"
            CREATE TABLE IF NOT EXISTS SessionNotes (
                Id INTEGER NOT NULL CONSTRAINT PK_SessionNotes PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL DEFAULT '',
                Content TEXT NOT NULL DEFAULT '',
                SessionDate TEXT NOT NULL DEFAULT '2024-01-01',
                UpdatedAt TEXT NOT NULL DEFAULT '2024-01-01'
            )");
    }
    catch { /* Table already exists */ }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();