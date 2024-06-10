using Basekeeper.Diagnostics;
using Basekeeper.Repository;
using Basekeeper.Repository.Yaml;
using Web.Pages;

ConsoleLogger.Init();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<InventoryRepository, YamlInventoryRepository>();
builder.Services.AddSingleton<OrderRepository, YamlOrderRepository>();
builder.Services.AddSingleton<RecipeRepository, YamlRecipeRepository>();
builder.Services.AddSingleton<CommandFactory, CommandFactory>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173").WithMethods("GET", "POST", "PUT", "DELETE");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
