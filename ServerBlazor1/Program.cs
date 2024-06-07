using Microsoft.AspNetCore.ResponseCompression;
using ServerBlazor1.Hubs;
using MessageSynchroniser.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddResponseCompression(options =>
{
	options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application.octet-stream" });
});
builder.Services.AddSignalR();

builder.Services.AddPersistentServices(builder.Configuration);

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

app.MapHub<ChatHub>("/chatHub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
