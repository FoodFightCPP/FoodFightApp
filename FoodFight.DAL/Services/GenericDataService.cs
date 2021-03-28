using FoodFight.Domain.Models;
using FoodFight.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FoodFight.DAL.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {

        string BASEURL = "https://foodfightwebapi.azurewebsites.net/api/";
        HttpClient client = new HttpClient();

        //readonly DbContextFactory _dBContextFactory;

        //public GenericDataService(DbContextFactory dBContextFactory)
        //{
        //    _dBContextFactory = dBContextFactory;
        //}

        //public async Task<T> Create(T entity)
        //{

        //    //using (FoodFightDbContext context = _dBContextFactory.CreateDbContext())
        //    //{
        //    //    EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
        //    //    await context.SaveChangesAsync();

        //    //    return createdResult.Entity;
        //    //}
        //}

    //public async Task<bool> Delete(Guid id)
    //{
    //    using (FoodFightDbContext context = _dBContextFactory.CreateDbContext())
    //    {
    //        T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
    //        context.Set<T>().Remove(entity);
    //        await context.SaveChangesAsync();

    //        return true;
    //    }
    //}

    public async Task<T> Get(Guid id, string type)
        {
            var json = await client.GetStringAsync(BASEURL + type +"/" + id);
            var getUser = JsonConvert.DeserializeObject<T>(json);
            return getUser;
        }

        public async Task<IEnumerable<T>> GetAll(string type)
        {
            var json = await client.GetStringAsync(BASEURL + type +"/");
            var getEntities = JsonConvert.DeserializeObject<T[]>(json);
            return getEntities;

        }

        //public async Task<T> Update(Guid id, T entity)
        //{
        //    using (FoodFightDbContext context = _dBContextFactory.CreateDbContext())
        //    {
        //        entity.Id = id;
        //        context.Set<T>().Update(entity);
        //        await context.SaveChangesAsync();

        //        return entity;
        //    }
        //}
    }
}
