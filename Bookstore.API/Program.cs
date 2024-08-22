using AutoMapper;
using Bookstore.BLL.Mappers;
using Bookstore.BLL.Services.UserService;
using Bookstore.BLL.Helpers;
using Bookstore.BLL.Services.AuthorService;
using Bookstore.BLL.Services.BookService;
using Bookstore.BLL.Services.ErrorService;
using Bookstore.BLL.Services.UserService;
using Bookstore.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var mapperConfig = new MapperConfiguration(cfg => {
    cfg.AddProfile<MappingProfile>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<ErrorHelper>();
builder.Services.AddScoped<IUserSessionService, UserSessionService>();
builder.Services.AddScoped<IErrorService, ErrorService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();