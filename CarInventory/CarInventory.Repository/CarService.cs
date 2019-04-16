using CarInventory.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarInventory.Repository
{
    public interface ICarService
        : IServiceBase<Cars, long>
    {
        List<Cars> GetCarsByUserId(string UserId);
    }
    public class CarService : ApplicationService<Cars, long>, ICarService
    {
        public CarService(ApplicationDbContext context) : base(context)
        {
           
        }

        public List<Cars> GetCarsByUserId(string UserId)
        {
            List<Cars> carList = (from c in Context.Cars
                                        where c.CreatedBy == UserId
                                        select new Cars
                                        {
                                            Id = c.Id,
                                            Brand = c.Brand,
                                            Model = c.Model,
                                            New = c.New,
                                            Price = c.Price,
                                            Year = c.Year                                            
                                        }).OrderBy(i => i.Brand).ToList();
            return carList;
        }
    }
}
