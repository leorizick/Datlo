using Datlo.Adapter.Out.FileProcessing;
using Datlo.Adapter.Out.Persistence.Repositories;
using Datlo.Application.Ports.In;
using Datlo.Application.Ports.Out;
using Datlo.Application.UseCases;
using Datlo.Configuration.Database;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<IImportFileUseCase, ImportFileUseCase>();
builder.Services.AddScoped<IProcessFile, CsvProcessor>();
builder.Services.AddScoped<IImportRepository, SqlServerImportRepository>();
builder.Services.AddScoped<ImportFileUseCase>();

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
