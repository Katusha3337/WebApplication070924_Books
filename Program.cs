using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Пример данных
var books = new List<Dictionary<string, string>>
{
    new Dictionary<string, string> { { "Id", "1" }, { "Title", "C# in Depth" }, { "Author", "Jon Skeet" }, { "ISBN", "9781617294532" } },
    new Dictionary<string, string> { { "Id", "2" }, { "Title", "Clean Code" }, { "Author", "Robert C. Martin" }, { "ISBN", "9780132350884" } },
    new Dictionary<string, string> { { "Id", "3" }, { "Title", "The Pragmatic Programmer" }, { "Author", "Andrew Hunt" }, { "ISBN", "9780201616224" } }
};

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/search", (string query) =>
{
    var results = books.Where(book =>
        book["Title"].Contains(query, System.StringComparison.OrdinalIgnoreCase) ||
        book["Author"].Contains(query, System.StringComparison.OrdinalIgnoreCase) ||
        book["ISBN"].Contains(query, System.StringComparison.OrdinalIgnoreCase)).ToList();

    return Results.Json(results);
});

app.Run();