using Marco.Forms.UI.Client.Components.Forms;
using Marco.Forms.UI.Client.Models;
using Marco.Forms.UI.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using Pure.Blazor.Components;
using DialogService = Microsoft.FluentUI.AspNetCore.Components.DialogService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// javascript
builder.Services.AddScoped<IElementUtils, ElementUtils>();

// services
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<Pure.Blazor.Components.DialogService>();
builder.Services.AddScoped<IDialogService, DialogService>();
builder.Services.AddScoped<IToastService, ToastService>();
builder.Services.AddScoped<ITooltipService, TooltipService>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddCascadingValue(sp =>
{
    var theme = new DefaultTheme();
    var source = new CascadingValueSource<PureTheme>(theme, isFixed: true);
    return source;
});

builder.Services.TryAddCascadingValue(_ => Theme.Auto);


builder.Services.AddSingleton<FormStore>();
builder.Services.AddSingleton<FormsClient>();
builder.Services.AddSingleton<ServerClient>();
// builder.Services.AddScoped<EditorInterop>();
// builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddScoped<IElementUtils, ElementUtils>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Marco.Forms.UI.Client._Imports).Assembly);

app.Run();