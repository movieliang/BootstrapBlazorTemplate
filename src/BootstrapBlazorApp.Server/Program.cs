using BootstrapBlazorApp.Server.Components;
using BootstrapBlazorApp.Server.Services;
using Microsoft.AspNetCore.SignalR;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddBootstrapBlazor();

builder.Services.AddCascadingAuthenticationState();

// 增加 DashboardDataService 服务
builder.Services.AddScoped<DashboardService>();

// 增加后台任务服务
builder.Services.AddTaskServices();
builder.Services.AddHostedService<ClearTempFilesService>();

// 增加 Pdf 导出服务
builder.Services.AddBootstrapBlazorTableExportService();

// 增加 Html2Pdf 服务
builder.Services.AddBootstrapBlazorHtml2PdfService();

// 增加 SignalR 服务数据传输大小限制配置
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStatusCodePagesWithReExecute("/NotFound", createScopeForStatusCodePages: false);
app.UseAntiforgery();
app.UseBootstrapBlazor();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();
