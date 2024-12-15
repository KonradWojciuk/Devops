var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Config app to listen on all interfaces
app.Urls.Add("http://0.0.0.0:5000");

app.MapGet("/", () => "Hello World!");

app.Run();
