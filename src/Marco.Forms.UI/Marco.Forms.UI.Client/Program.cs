using Marco.Forms.UI.Client.Components.Forms;
using Marco.Forms.UI.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Pure.Blazor.Components;
using DialogService = Microsoft.FluentUI.AspNetCore.Components.DialogService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// javascript
builder.Services.AddScoped<IElementUtils, ElementUtils>();

// services
builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<Pure.Blazor.Components.DialogService>();
builder.Services.AddScoped<IDialogService, DialogService>();

// theme
builder.Services.AddCascadingValue(_ =>
{
    var theme = new DefaultTheme();
    var source = new CascadingValueSource<PureTheme>(theme, isFixed: false);
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

await builder.Build().RunAsync();