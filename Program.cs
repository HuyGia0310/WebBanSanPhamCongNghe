using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebBanSanPhamCongNghe.Areas.Admin.Data;
using WebBanSanPhamCongNghe.Helper;
using WebBanSanPhamCongNghe.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyShopContext>();

builder.Services.AddTransient<EmailService>();

// Use Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Customer/SignIn";
        option.AccessDeniedPath = "/AccessDenied";
    });

//Cho phép Field Null trong Form
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


// Use Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MyShopContext>();

    await dbContext.Database.ExecuteSqlRawAsync($@"
IF COL_LENGTH('dbo.payment', 'status') IS NULL
BEGIN
    ALTER TABLE [dbo].[payment]
    ADD [status] NVARCHAR(50) NOT NULL CONSTRAINT [DF_payment_status] DEFAULT (N'{MyConst.PAYMENT_DEFAULT_STATUS}') WITH VALUES;
END
ELSE
BEGIN
    -- Sử dụng EXEC để bọc câu lệnh UPDATE lại thành một chuỗi văn bản
    EXEC('UPDATE [dbo].[payment] SET [status] = N''{MyConst.PAYMENT_DEFAULT_STATUS}'' WHERE [status] IS NULL OR LTRIM(RTRIM([status])) = N''''');
END");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
