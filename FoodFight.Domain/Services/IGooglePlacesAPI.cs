using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodFight.Domain.Services
{
    public interface IGooglePlacesAPI<T>
    {
        Task<T> GetAllLocations(string lat, string lng, int distance = 1500);
    }
}
