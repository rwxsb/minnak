using LiteDB;
using Microsoft.OpenApi.Models;
using minnak.Controllers;
using minnak.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "minnak", Version = "v1" });
});

builder.Services.AddSingleton<ILiteDatabase,LiteDatabase>(_ => new LiteDatabase("short-links.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "minnak v1");
            c.RoutePrefix = "";
        });
}

app.UseRouting();

app.UseEndpoints((endpoints) =>{
    // endpoints.MapGet("/", ctx =>{
    //     return ctx.Response.SendFileAsync(@"./wwwroot/index.html");
    // });

    //map endpoints and handle them
    endpoints.MapFallback((context) => {
            var db = context.RequestServices.GetService<ILiteDatabase>();
            var collection = db.GetCollection<ShortLink>("ShortLinks");

            var id = context.Request.Path.ToUriComponent().Trim('/');
            var entry = collection.Find(p => p.Id == id).FirstOrDefault();
    
            if (entry != null)
                context.Response.Redirect(entry.Url.Replace("%2F","/"));
            else
                context.Response.Redirect("/");

            return Task.CompletedTask;
    });

});

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
