using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data;
using TicketingSystem.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddDbContext<TicketingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TicketingDb")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();