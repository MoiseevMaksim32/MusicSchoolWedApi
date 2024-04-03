using Microsoft.EntityFrameworkCore;
using MusicSchool1.Connectios;
using MusicSchool1.Models;
using Swashbuckle.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;

namespace MusicSchool1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            // добавление таблицы в контекст ASP.NET
            builder.Services.AddDbContext<SpecialityDB>();
            builder.Services.AddDbContext<GenresDB>();
            builder.Services.AddDbContext<PositionDB>();
            builder.Services.AddDbContext<EmployeeDB>();
            builder.Services.AddDbContext<GroupMusicDB>();
            builder.Services.AddDbContext<StudentDB>();
            builder.Services.AddDbContext<ConcertDB>();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();



            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(); // подключение свагера 
                app.UseSwaggerUI();
            }

            app.MapEntityEndpoints<Speciality, SpecialityDB>("/speciality");
            app.MapEntityEndpoints<Genres, GenresDB>("/genres");
            app.MapEntityEndpoints<Position, PositionDB>("/position");
            app.MapEntityEndpoints<Employee, EmployeeDB>("/employee");
            app.MapEntityEndpoints<GroupMusic, GroupMusicDB>("/group_music");
            app.MapEntityEndpoints<Student, StudentDB>("/student");
            app.MapEntityEndpoints<Concert, ConcertDB>("/concert");

            app.Run();
        }
    }

    // Дальше бога нет
        public static class EntityEndpoints
        {
            public static void MapEntityEndpoints<TEntity, TDbContext>(this WebApplication app, string routePrefix)
              where TEntity : class, IEntity
              where TDbContext : DbContext
            {
                app.MapGet(routePrefix, async (TDbContext dbContext) =>
                {
                    var entities = await dbContext.Set<TEntity>().ToListAsync();
                    return entities;
                });

            app.MapGet($"{routePrefix}/{{ID}}", async (int id, TDbContext dbContext) =>
            {
                var entity = await dbContext.Set<TEntity>().FindAsync(id);
                if (entity == null) return Results.NotFound();
                return Results.Ok(entity);
            });
            // Надо ли вообще обращаться по id? 
            app.MapPost(routePrefix, async (TDbContext dbContext, TEntity entity) =>
            {
                dbContext.Set<TEntity>().Add(entity);
                await dbContext.SaveChangesAsync();
                return Results.Created($"{routePrefix}/{entity.ID}", entity);
            });

            app.MapPut($"{routePrefix}/{{ID}}", async(int id, TDbContext dbContext, TEntity entit) =>
                {
                    var existingEntity = await dbContext.Set<TEntity>().FindAsync(id);
                    if (existingEntity == null) { return Results.NotFound(); }
                    dbContext.Entry(existingEntity).CurrentValues.SetValues(entit);
                    await dbContext.SaveChangesAsync();
                    return Results.Ok(entit);
                });

            app.MapDelete($"{routePrefix}/{{ID}}", async(int id, TDbContext dbContext) =>
            {
                if(await dbContext.Set<TEntity>().FindAsync(id) is TEntity entity)
                {
                    dbContext.Set<TEntity>().Remove(entity);
                    await dbContext.SaveChangesAsync();
                    return Results.NoContent();
                }
                return Results.NotFound();
            });
        }
       
    }
}