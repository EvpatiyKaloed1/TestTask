using Application.Commons.Interfaces;
using Infrastructure;
using Infrastructure.Repositories.Clients;
using Infrastructure.Repositories.Founders;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
});
builder.Services.RegisterMapster();

builder.Services.AddMediatR(assembly =>
{
    assembly.RegisterServicesFromAssembly(typeof(Application.Commons.AssemblyReference).Assembly);
});

builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IFounderRepository, FounderRepository>();

builder.Services.AddDbContext<Database>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();