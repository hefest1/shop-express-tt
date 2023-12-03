using Data;
using Data.Repositories;
using Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ToDoDBContext, ToDoDBContext>();
builder.Services.AddScoped<ITaskRepository, TasksEFCoreRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//test
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var toDoDbContext = services.GetRequiredService<ToDoDBContext>();
    //toDoDbContext.Database.EnsureDeleted();
    toDoDbContext.Database.EnsureCreated();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
