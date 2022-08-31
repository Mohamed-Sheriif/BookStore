using BookStore.Models.Repositories;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//Add MVc
builder.Services.AddMvc();
//Add Dependencies For Author And BookRepo
builder.Services.AddScoped<IBookStoreRepo<Author>, AuthorDbRepo>();
builder.Services.AddScoped<IBookStoreRepo<Book> , BookDbRepo>();

//Add Connectionstring 
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Author}/{action=Index}/{id?}");

app.Run();
