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

        public async Task<T> Create(T entity, string type)
        {

            var json = JsonConvert.SerializeObject(entity);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync(BASEURL + type + "/", httpContent);
                
            return entity;
        }

        public async Task<bool> Delete(Guid id, string type)
        {
            var response = await client.DeleteAsync(BASEURL + type + "/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

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

        public async Task<T> Update(Guid id, T entity, string type)
        {
            var json = JsonConvert.SerializeObject(entity);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            string url = BASEURL + type + "/" + id.ToString();
            var response = await client.PutAsync(BASEURL + type + "/" + id.ToString(), httpContent);
            return entity;
        }
    }
}
