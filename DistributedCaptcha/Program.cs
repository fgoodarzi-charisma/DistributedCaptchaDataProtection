using DNTCaptcha.Core;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var redisConn = "127.0.0.1:6379,allowAdmin=true";
var redis = ConnectionMultiplexer.Connect(redisConn);
builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(redis, "data_protection_keys")
    .SetApplicationName("product");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConn;
    options.InstanceName = "dnt__";
});

builder.Services.AddDNTCaptcha(options =>
{
    options.UseDistributedCacheStorageProvider()
        .AbsoluteExpiration(minutes: 1)
        .ShowThousandsSeparators(false)
        .WithNoise(0.99f, 0.99f, 1, 0.0f)
        .WithEncryptionKey("key");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
