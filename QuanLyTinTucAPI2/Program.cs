using Microsoft.EntityFrameworkCore;
using QuanLyTinTucAPI2;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Giữ nguyên tên thuộc tính
        options.JsonSerializerOptions.WriteIndented = true; // Định dạng JSON dễ đọc
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Xử lý vòng lặp tham chiếu
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=DUSTIN;Encrypt=false;TrustServerCertificate=True;Database=QuanLyTinTuc;User Id=sa;Password=123456789;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

// Middleware xử lý lỗi
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\": \"Lỗi server không xác định. Vui lòng kiểm tra lại.\"}");
    });
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();