using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI {
    class Program {
        static void Main(string[] args) {

            CarManager carManager = new CarManager(new InMemoryCarDal());
            Car car = new Car { Id = 5, BrandId = 3, ColorId = 5, ModelYear = 2012, DailyPrice = 5000, Description = "" };
            
            
            carManager.Add(car);

            carManager.Delete(new Car { Id = 1 });


            foreach (var c in carManager.GetAll()) {
                Console.WriteLine("{0} {1}", c.Id, c.DailyPrice);
            }

            Console.WriteLine(carManager.GetById(4).DailyPrice);


        }
    }
}
