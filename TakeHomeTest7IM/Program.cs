using NLog.Extensions.Logging;
using TakeHomeTest7IM.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.SetMinimumLevel(LogLevel.Trace);
    builder.AddNLog();
});

var dataFolderPath = Path.Combine(AppContext.BaseDirectory, "App_Data");
builder.Services.AddSingleton<IDataService>(provider =>
{
    var jsonFilePath = Path.Combine(dataFolderPath, "persons.json");
    return new DataService(jsonFilePath);
});

builder.Services.AddScoped<ISearchService, SearchService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
